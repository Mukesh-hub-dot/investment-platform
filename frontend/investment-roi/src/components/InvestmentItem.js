// src/components/InvestmentItem.js
import React from "react";

function InvestmentItem({ index, data, onChange, onRemove }) {
  const investmentOptions = [
    "Cash",
    "Shares",
    "FixedInterest",
    "TermDeposit",
    "ManagedFunds",
    "ETF",
    "InvestmentBonds",
    "Annuities",
    "LIC",
    "REIT"
  ];

  return (
    <div className="investment-row">
      <select
        value={data.type}
        onChange={(e) => onChange(index, "type", e.target.value)}
        required
      >
        <option value="">Select Type</option>
        {investmentOptions.map((type) => (
          <option key={type} value={type}>
            {type}
          </option>
        ))}
      </select>

      <input
        type="number"
        placeholder="Percentage"
        value={data.percentage}
        onChange={(e) => onChange(index, "percentage", e.target.value)}
        required
      />

      <button type="button" onClick={() => onRemove(index)}>
        ❌
      </button>
    </div>
  );
}

export default InvestmentItem;
