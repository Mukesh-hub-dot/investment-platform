import React from "react";
import "./InvestmentForm.css";
import InvestmentRow from "./InvestmentRow";

const defaultAmount = 100000;
const validTypes = [
  "Cash", "Shares", "FixedInterest", "TermDeposit", "ManagedFunds",
  "ETF", "InvestmentBonds", "Annuities", "LIC", "REIT"
];

const InvestmentForm = ({
  onApiResponse,
  onReset,
  investments,
  setInvestments,
  totalAmount,
  setTotalAmount,
}) => {
  const [isSubmitting, setIsSubmitting] = React.useState(false);
  const [error, setError] = React.useState("");

  const getRemainingPercentage = () => {
    const total = investments.reduce((sum, inv) => {
      const val = parseFloat(inv.percentage);
      return sum + (isNaN(val) ? 0 : val);
    }, 0);
    return Math.max(0, 100 - total);
  };

  const getRemainingAmount = () => {
    const allocated = investments.reduce((sum, inv) => {
      const perc = parseFloat(inv.percentage);
      return sum + (isNaN(perc) ? 0 : (totalAmount * perc) / 100);
    }, 0);
    return Math.max(0, totalAmount - allocated);
  };

  const getAvailableTypes = (currentIndex) => {
    const selectedTypes = investments.map((inv, i) => i !== currentIndex && inv.type).filter(Boolean);
    return validTypes.filter(type => !selectedTypes.includes(type));
  };

  const handleInvestmentChange = (index, updatedInvestment) => {
    const updatedInvestments = [...investments];
    updatedInvestments[index] = updatedInvestment;
    setInvestments(updatedInvestments);
  };

  const handleAddInvestment = () => {
    const usedTypes = investments.map((i) => i.type).filter(Boolean);
    if (usedTypes.length >= validTypes.length) {
      setError("All investment types are already selected.");
      return;
    }

    if (getRemainingPercentage() <= 0) {
      setError("No remaining percentage available.");
      return;
    }

    setInvestments([...investments, { type: "", percentage: "" }]);
    setError("");
  };

  const handleRemoveInvestment = (index) => {
    const updated = [...investments];
    updated.splice(index, 1);
    setInvestments(updated);
    setError("");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    const filledInvestments = investments.filter(inv => inv.type && inv.percentage);

    if (filledInvestments.length === 0) {
      setError("Please select at least one valid investment.");
      return;
    }

    const totalPerc = filledInvestments.reduce((sum, inv) => sum + parseFloat(inv.percentage || 0), 0);
    if (totalPerc > 100) {
      setError("Total percentage cannot exceed 100%");
      return;
    }

    const payload = {
      totalAmount,
      investments: filledInvestments.map((inv) => ({
        type: inv.type,
        percentage: parseFloat(inv.percentage),
      })),
    };

    try {
      setIsSubmitting(true);
      const response = await fetch("http://localhost:5092/api/investment/calculate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      const data = await response.json();
      onApiResponse(data);
    } catch (err) {
      console.error("API error:", err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="investment-form">
      <div className="form-group">
        <label>Total Investment Amount (AUD):</label>
        <input
          type="number"
          value={totalAmount}
          min={0}
          onChange={(e) => setTotalAmount(Number(e.target.value))}
        />
      </div>

      <div className="investment-section">
        <label>Investments:</label>
        {investments.map((investment, index) => (
          <InvestmentRow
            key={index}
            investment={investment}
            onChange={(inv) => handleInvestmentChange(index, inv)}
            onRemove={() => handleRemoveInvestment(index)}
            canRemove={investments.length > 1}
            availableTypes={getAvailableTypes(index)}
          />
        ))}
      </div>

      <p className="remaining">Remaining Percentage: {getRemainingPercentage()}%</p>
      <p className="remaining">Remaining Balance: AUD {getRemainingAmount().toFixed(2)}</p>

      {error && <div className="error-message">{error}</div>}

      <div className="button-group">
        <button
          type="button"
          onClick={handleAddInvestment}
          disabled={investments.length >= validTypes.length || getRemainingPercentage() <= 0}
          className="btn-add"
        >
          ➕ Add Investment
        </button>

        <button type="submit" className="btn-submit" disabled={isSubmitting}>
          {isSubmitting ? "Calculating..." : "Calculate"}
        </button>

        <button type="button" className="btn-reset" onClick={onReset}>
          🔁 Reset
        </button>
      </div>
    </form>
  );
};

export default InvestmentForm;
