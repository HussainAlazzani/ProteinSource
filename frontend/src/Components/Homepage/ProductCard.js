import React from "react";
import agent from "../../Adapters/agent";

const ProductCard = ({ product }) => {
  const {
    id,
    name,
    flavour,
    price,
    previousPrice,
    description,
    weight,
    imageUrl,
    rating,
    brand,
  } = product;
  return (
    <div className="product-card">
      {product.categories.includes(8) && (
        <div className="product-card__newTag">New</div>
      )}
      <img
        className="product-card__image"
        src={agent.baseUrl + imageUrl}
        alt="Product Image"
      />
      <h5 className="product-card__brand">{brand}</h5>
      <h5 className="product-card__name">{name}</h5>
      <div>
        <p className="product-card__price">£{price}</p>
        <p className="product-card__previousPrice">£{previousPrice}</p>
      </div>
    </div>
  );
};

export default ProductCard;

/* 
  <h4>id: {id}</h4>
  <h4>brand: {brand}</h4>
  <h4>name: {name}</h4>
  <h4>flavour: {flavour}</h4>
  <h4>weight: {weight}</h4>
  <h4>price: {price}</h4>
  <h4>previousPrice: {previousPrice}</h4>
  <h4>rating: {rating}</h4>
  <h4>description: {description}</h4>
  <h4>{imageUrl}</h4>
*/
