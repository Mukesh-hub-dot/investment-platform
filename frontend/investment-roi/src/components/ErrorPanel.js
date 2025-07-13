import React from "react";

function ErrorPanel({ error }) {
  return (
    <div style={{ marginTop: "20px", padding: "10px", border: "1px solid red", color: "red" }}>
      <h3>Validation Error</h3>
      {error?.messages?.map((msg, i) => (
        <p key={i}>{msg}</p>
      ))}
    </div>
  );
}

export default ErrorPanel;
