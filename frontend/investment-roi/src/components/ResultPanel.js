import React from "react";
import "./ResultPanel.css";

const ResultPanel = ({ result }) => {
  return (
    <div className="result-panel">
      <h2>Investment ROI</h2>

      <div className="result-box">
        <div className="result-row">
          <span className="label">Total Projected Return (AUD):</span>
          <span className="value">${result.totalProjectedReturnAUD.toFixed(2)}</span>
        </div>
        <div className="result-row">
          <span className="label">Total Fee (AUD):</span>
          <span className="value">${result.totalFeeAUD.toFixed(2)}</span>
        </div>
        <div className="result-row">
          <span className="label">Total Projected Return (USD):</span>
          <span className="value">${result.totalProjectedReturnUSD.toFixed(2)}</span>
        </div>
      </div>
    </div>
  );
};

export default ResultPanel;
