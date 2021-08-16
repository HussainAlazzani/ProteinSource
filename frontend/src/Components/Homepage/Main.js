import { filter } from "async";
import React, { useEffect, useState } from "react";
import agent from "../../Adapters/agent";
import ProductCard from "./ProductCard";

const Main = () => {
  const [products, setProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState([]);

  useEffect(() => {
    const fetchProducts = async () => {
      const data = await agent.products.filteredProducts({
        pageIndex: 1,
        pageSize: 10,
        // categoryId: 1,
        // sort: "a",
        // search: "USN 100% Premium Whey Protein",
      });
      setProducts(data);
      console.log(data.data);
      setFilteredProducts(data.data);
    };
    fetchProducts();
  }, []);

  return (
      <div class="product-list">
        {/* {
          <li>
            {products.pageIndex +
              ", " +
              products.pageSize +
              ", " +
              products.count}
          </li>
        } */}
        {filteredProducts.map((filtered) => (
          <ProductCard key={filtered.id} product={filtered} />
        ))}
      </div>
  );
};

export default Main;
