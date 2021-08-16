import React, { useState } from "react";
import Carousel from "react-material-ui-carousel";
import { Paper, Button } from "@material-ui/core";
import agent from "../../Adapters/agent.js";
import "../../Styles/styles.css";
import { useEffect } from "react";
import items from "./promoItems.json";

// https://www.npmjs.com/package/react-material-ui-carousel


const Promotions = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Fetch products from API.
    const fetchProducts = async () => {
      const data = await agent.products.all();
      setProducts(data);
      setLoading(false);
    };
    fetchProducts();
  }, []);

  // var items = [
  //   {
  //     image: agent.baseUrl + products[1]?.imageUrl,
  //     name: "Whey",
  //     description: "Grab a bargain",
  //   },
  //   {
  //     image: agent.baseUrl + products[0]?.imageUrl,
  //     name: "Vegan",
  //     description: "Sale Sale Sale",
  //   },
  // ];

  return (
    <div class="promotions">
      {loading ? (
        <h1>Loading.........</h1>
      ) : (
        <Carousel>
          {items.map((item, i) => {
            return <Item key={i} item={item} />;
          })}
        </Carousel>
      )}
    </div>
  );
};

function Item({ item }) {
  return (
      <Paper>
        <img
          src={item.image}
          alt="server not connected"
        />
        <h2>{item.name}</h2>
        <p>{item.description}</p>

        <Button className="CheckButton">Click Me</Button>
      </Paper>
  );
}

export default Promotions;
