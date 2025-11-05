import React, { useState } from 'react';

export default function ProductForm({onAdd}){
  const [name, setName] = useState('');
  const [quantity, setQuantity] = useState(0);
  const [price, setPrice] = useState(0.0);

  const submit = (e) => {
    e.preventDefault();
    if(!name) return alert('Name required');
    onAdd({ name, quantity: Number(quantity), price: Number(price) });
    setName(''); setQuantity(0); setPrice(0.0);
  };

  return (
    <form onSubmit={submit} style={{marginBottom:20}}>
      <input placeholder="Name" value={name} onChange={e=>setName(e.target.value)} />
      <input type="number" placeholder="Quantity" value={quantity} onChange={e=>setQuantity(e.target.value)} style={{marginLeft:8}}/>
      <input type="number" step="0.01" placeholder="Price" value={price} onChange={e=>setPrice(e.target.value)} style={{marginLeft:8}}/>
      <button type="submit" style={{marginLeft:8}}>Add</button>
    </form>
  );
}
