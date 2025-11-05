const API_BASE = process.env.REACT_APP_API_URL || 'http://localhost:5000';

async function request(path, options = {}){
  try {
    const res = await fetch(API_BASE + path, options);
    const json = await res.json();
    return json;
  } catch(e){
    console.error('API error', e);
    return null;
  }
}

export default {
  getProducts: () => request('/api/products'),
  createProduct: (p) => request('/api/products', {
    method: 'POST',
    headers: {'Content-Type': 'application/json', 'X-API-KEY': 'inventory-secret'},
    body: JSON.stringify(p)
  }),
  updateProduct: (id, p) => request(`/api/products/${id}`, {
    method: 'PUT',
    headers: {'Content-Type': 'application/json', 'X-API-KEY': 'inventory-secret'},
    body: JSON.stringify(p)
  }),
  deleteProduct: (id) => request(`/api/products/${id}`, {
    method: 'DELETE',
    headers: {'X-API-KEY': 'inventory-secret'}
  })
};
