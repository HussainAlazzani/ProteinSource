import React, { useEffect, useState } from "react";
import "../../Styles/styles.css";
import agent from "../../Adapters/agent";

const BrandsFeatured = () => {
  const [brands, setBrands] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  const getFeaturedBrands = (data) => {
    const max = 5;
    if (data.length <= max) return data;

    // Algorithm that randomly selects brands up to the max value without duplication.
    let featuredBrands = [];
    let index = [];
    for (let i = 0; i < max; i++) {
      let random = Math.floor(Math.random() * max);
      index.includes(random) ? i-- : index.push(random);
    }

    for (let i = 0; i < index.length; i++) {
      featuredBrands.push(data[index[i]]);
    }
    return featuredBrands;
  };

  useEffect(() => {
    setIsLoading(true);
    const fetchBrands = async () => {
      setIsLoading(true);
      const data = await agent.products.brands();
      const featuredBrands = getFeaturedBrands(data);
      setBrands(featuredBrands);
      setIsLoading(false);
    };
    fetchBrands();
  }, []);

  return (
    <div className="featured-brands">
      {isLoading ? (
        <h1>Loading...</h1>
      ) : (
        brands.map((brand) => {
          return (
            <div key={brand.id} className="featured-brands__card">
              <img src={agent.baseUrl + brand.imageUrl} alt="brand logo" />
              <button className="button" style={{ verticalAlign: "middle" }}>
                <span>{brand.name}</span>
              </button>
            </div>
          );
        })
      )}
    </div>
  );
};

export default BrandsFeatured;
