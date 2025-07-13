import React from "react";
import "./Tabs.css";

const Tabs = ({ activeTab, setActiveTab }) => {
  return (
    <div className="tabs-container">
      <button
        className={activeTab === "investment" ? "tab active" : "tab"}
        onClick={() => setActiveTab("investment")}
      >
        Investment Options
      </button>
      <button
        className={activeTab === "roi" ? "tab active" : "tab"}
        onClick={() => setActiveTab("roi")}
      >
        ROI
      </button>
    </div>
  );
};

export default Tabs;
