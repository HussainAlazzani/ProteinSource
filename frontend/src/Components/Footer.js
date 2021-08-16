import React from "react";
import { Link } from "react-router-dom";
import "../Styles/styles.css";
import mastercard from "../images/mastercard.png";
import visa from "../images/visa.png";
import paypal from "../images/paypal.png";
import americanexpress from "../images/americanexpress.png";
import visaElectron from "../images/visaelectron.png";
import maestro from "../images/maestro.png";

const Footer = () => {
  return (
    <>
      <footer className="footer">
        <div className="footer__section">
          <h4>OUR PROMISE</h4>
          <p>
            With a large stock of supplements, if our website says ‘in stock’,
            we'll be packing and dispatching your items as soon as possible
          </p>
        </div>
        <div className="footer__section">
          <h4>INFORMATION</h4>
          <ul>
            <Link className="footer__link" to="/about">
              <li>About Us</li>
            </Link>
            <Link className="footer__link" to="/contact">
              <li>Contact Us</li>
            </Link>
            <Link className="footer__link" to="/refund">
              <li>Refund Policy</li>
            </Link>
            <Link className="footer__link" to="/shipping">
              <li>Shipping Policy</li>
            </Link>
            <Link className="footer__link" to="/privacy">
              <li>Privacy Policy</li>
            </Link>
            <Link className="footer__link" to="/termsofservice">
              <li>Terms of Service</li>
            </Link>
          </ul>
        </div>
        <div className="footer__section">
          <div className="footer_followUs">
            <h4>FOLLOW US</h4>
            <Link
              className="footer__socialMedia"
              to={{ pathname: "https://www.facebook.com" }}
              target="_blank"
            >
              <i className="fab fa-facebook fa-2x"></i>
            </Link>
            <Link
              className="footer__socialMedia"
              to={{ pathname: "https://www.twitter.com" }}
              target="_blank"
            >
              <i className="fab fa-twitter fa-2x"></i>
            </Link>
            <Link
              className="footer__socialMedia"
              to={{ pathname: "https://www.instagram.com" }}
              target="_blank"
            >
              <i className="fab fa-instagram fa-2x"></i>
            </Link>
            <Link
              className="footer__socialMedia"
              to={{ pathname: "https://www.pininterest.com" }}
              target="_blank"
            >
              <i className="fab fa-pinterest fa-2x"></i>
            </Link>
          </div>
          <div className="footer_weAccept">
            <h4>WE ACCEPT</h4>
            <img
              className="footer__paymentLogo"
              src={paypal}
              alt="PayPal logo"
            />
            <img className="footer__paymentLogo" src={visa} alt="Visa logo" />
            <img
              className="footer__paymentLogo"
              src={mastercard}
              alt="Mastercard logo"
            />
            <img
              className="footer__paymentLogo"
              src={visaElectron}
              alt="Visa Electron logo"
            />
            <img
              className="footer__paymentLogo"
              src={maestro}
              alt="Maestro logo"
            />
            <img
              className="footer__paymentLogo"
              src={americanexpress}
              alt="American Express logo"
            />
          </div>
        </div>
      </footer>
    </>
  );
};

export default Footer;
