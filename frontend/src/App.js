import React from "react";
import { useState } from "react";
import agent from "./Adapters/agent.js";
import { useEffect } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

// import "./Styles/app.css";
import Header from "./Components/Header.js";
import Home from "./Components/Homepage/Home.js";
import Footer from "./Components/Footer.js";

const App = () => {
  return (
    <>
      <Router>
          <Switch>
            <Route path="/">
              <Header />
              <Home />
              <Footer />
            </Route>
          </Switch>  
      </Router>
    </>
  );
};

export default App;
