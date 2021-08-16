import React from "react";
import Navbar from "./Navbar";
import Promotions from "./Promotions";
import Main from "./Main";
import Trending from "./Trending";
import BrandsFeatured from "./BrandsFeatured";

import "../../Styles/styles.css";

const Home = () => {
  return (
    <div className="home-content">
        <Navbar />
        <Promotions />
        <Main />
        <BrandsFeatured />
      {/* <Trending /> */}
    </div>
  );
};

export default Home;
