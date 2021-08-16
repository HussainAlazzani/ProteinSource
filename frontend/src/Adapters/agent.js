import axios from "axios";

const sleep = (delay) => {
  return new Promise((resolve) => setTimeout(resolve, delay));
};

axios.defaults.baseURL = "https://localhost:5001/api";

axios.interceptors.response.use(async (res) => {
  try {
    await sleep(1000);
    return res;
  } catch (err) {
    console.log(err);
    return await Promise.reject(err);
  }
});

const requests = {
  get: (url) => axios.get(url).then((res) => res.data),
  post: (url, body) => axios.post(url, body).then((res) => res.data),
  put: (url, body) => axios.put(url, body).then((res) => res.data),
  delete: (url) => axios.delete(url).then((res) => res.data),
};

const products = {
  all: () => requests.get("/products"),
  single: (id) => requests.get(`/products/${id}`),

  brands: () => requests.get("/products/brands"),
  categories: () => requests.get("/products/categories"),
  category: (id) => requests.get(`/products/categories/${id}`),

  filteredProducts: (params) => {
    if (params.pageIndex < 1) params.pageIndex = 1;
    if (params.pageSize < 1) params.pageSize = 1;

    let apiUrl = `/products/filtered?PageIndex=${params.pageIndex}&PageSize=${params.pageSize}`;

    if (params.categoryId !== undefined) apiUrl += `&CategoryId=${params.categoryId}`;
    if (params.sort !== undefined) apiUrl += `&Sort=${params.sort}`;
    if (params.search !== undefined) apiUrl += `&Search=${params.search}`;

    return requests.get(apiUrl);
  },
  // byCategory: (params) => requests.get("/products/categories/filtered"),

  // create: (product) => requests.post("/products/", product),
  // update: (product) => requests.put(`/products/${product.id}`, product),
  // delete: (id) => requests.delete(`/products/${id}`),
};

const agent = { products, baseUrl: axios.defaults.baseURL };

export default agent;
