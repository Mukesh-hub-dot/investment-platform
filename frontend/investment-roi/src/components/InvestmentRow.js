import React from "react";

const InvestmentRow = ({ investment, onChange, onRemove, canRemove, availableTypes }) => {
  const handleTypeChange = (e) => {
    onChange({ ...investment, type: e.target.value });
  };

  const handlePercentageChange = (e) => {
    onChange({ ...investment, percentage: e.target.value });
  };

  return (
    <div className="investment-row form-group">
      <select className="invsetment-control"  value={investment.type} onChange={handleTypeChange}>
        <option value="">--Select--</option>
        {availableTypes.map((type) => (
          <option key={type} value={type}>{type}</option>
        ))}
      </select>

      <input
        type="number"
        placeholder="%"
        value={investment.percentage}
        onChange={handlePercentageChange}
        min="0"
        max="100"
      />

      {canRemove && (
        <button type="button" className="btn-remove" onClick={onRemove} style={{ marginLeft: "10px" }}>
          ❌
        </button>
      )}
    </div>
  );
};

export default InvestmentRow;
