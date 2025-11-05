import React, { useEffect, useState } from 'react';
import ProductList from './components/ProductList';
import ProductForm from './components/ProductForm';
import api from './api';

export default function App(){
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  const load = async () => {
    setLoading(true);
    const res = await api.getProducts();
    if(res && res.success) setProducts(res.data);
    setLoading(false);
  };

  useEffect(()=>{ load(); }, []);

  const add = async (p) => {
    const created = await api.createProduct(p);
    if(created && created.success){
      setProducts(prev=>[...prev, created.data]);
    }
  };

  const update = async (id, p) => {
    const updated = await api.updateProduct(id, p);
    if(updated && updated.success){
      setProducts(prev=>prev.map(x=> x.id === id ? updated.data : x));
    }
  };

  const remove = async (id) => {
    const deleted = await api.deleteProduct(id);
    if(deleted && deleted.success){
      setProducts(prev=>prev.filter(x=> x.id !== id));
    }
  };

  return (
    <div style={{padding:20, fontFamily: 'Arial'}}>
      <h1>InventoryHub</h1>
      <ProductForm onAdd={add} />
      {loading ? <p>Loading...</p> : <ProductList products={products} onUpdate={update} onDelete={remove} />}
    </div>
  );
}
