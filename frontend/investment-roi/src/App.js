import React, { useState } from "react";
import "./App.css";
import InvestmentForm from "./components/InvestmentForm";
import ResultPanel from "./components/ResultPanel";
import ErrorPanel from "./components/ErrorPanel";
import Tabs from "./components/Tabs";

function App() {
  const [apiResponse, setApiResponse] = useState(null);
  const [activeTab, setActiveTab] = useState("investment");

  // State for form inputs
  const [investments, setInvestments] = useState([{ type: "", percentage: "" }]);
  const [totalAmount, setTotalAmount] = useState(100000);

  const handleApiResponse = (response) => {
    setApiResponse(response);
    setActiveTab("roi"); // Go to ROI tab on successful calculation
  };

  const handleReset = () => {
    setInvestments([{ type: "", percentage: "" }]);
    setTotalAmount(100000);
    setApiResponse(null);
    setActiveTab("investment");
  };

  return (
    <div className="app-container">
      <h1>Investment Portfolio Calculator</h1>
      <Tabs activeTab={activeTab} setActiveTab={setActiveTab} />
      {activeTab === "investment" && (
        <InvestmentForm
          onApiResponse={handleApiResponse}
          onReset={handleReset}
          investments={investments}
          setInvestments={setInvestments}
          totalAmount={totalAmount}
          setTotalAmount={setTotalAmount}
        />
      )}
      {activeTab === "roi" && apiResponse?.success && (
        <ResultPanel result={apiResponse.data} />
      )}
      {apiResponse?.error && <ErrorPanel error={apiResponse.error} />}
    </div>
  );
}

export default App;
