import React from 'react';

export default function ProductList({products, onUpdate, onDelete}){
  if(!products || products.length === 0) return <p>No products.</p>;
  return (
    <table border="1" cellPadding="8" style={{borderCollapse:'collapse', width:'100%', marginTop:10}}>
      <thead>
        <tr><th>Name</th><th>Qty</th><th>Price</th><th>Actions</th></tr>
      </thead>
      <tbody>
        {products.map(p=>(
          <tr key={p.id}>
            <td>{p.name}</td>
            <td>{p.quantity}</td>
            <td>${Number(p.price).toFixed(2)}</td>
            <td>
              <button onClick={()=> {
                const newQty = prompt('New quantity', p.quantity);
                if(newQty==null) return;
                const parsed = parseInt(newQty,10);
                if(isNaN(parsed)) return alert('Invalid number');
                onUpdate(p.id, {name: p.name, quantity: parsed, price: p.price});
              }}>Update Qty</button>
              <button onClick={()=>{ if(window.confirm('Delete?')) onDelete(p.id); }} style={{marginLeft:8}}>Delete</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
