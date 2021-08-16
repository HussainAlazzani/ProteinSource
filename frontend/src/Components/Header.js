import React from "react";
// import SearchIcon from "@material-ui/icons/Search";
import ShoppingBasketIcon from "@material-ui/icons/ShoppingBasket";
import logo from "../images/proteinsource_logo.png";

import "../Styles/styles.css";
import { Link } from "react-router-dom";

const Header = () => {
  let user = {
    email: "testinguser@test.com",
  };

  let basket = [];

  return (
    <header className="header">
      <Link to="/">
        <div className="header__logo">
          <img src={logo} alt="Company Logo" />
        </div>
      </Link>
      <div className="header__search">
        <input className="header__searchInput" type="text" />
        <button className="header__searchButton" type="submit">
          <i class="fas fa-search"></i>
        </button>
      </div>
      <Link to={user?.email && "/login"} style={{ textDecoration: "none" }}>
        <div className="header__account">{user ? user.email : "Login"}</div>
      </Link>
      <Link to="/checkout">
        <div className="header__basket">
          <div className="header_basketAmount">{basket?.length}</div>
          <div className="header_basketIcon">
            <ShoppingBasketIcon />
          </div>
        </div>
      </Link>
    </header>
  );
};

export default Header;
