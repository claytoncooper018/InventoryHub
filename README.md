# InventoryHub API (backend)

Minimal .NET 7 Web API for InventoryHub.

## Run
1. Install .NET 7 SDK.
2. From backend folder:
```
dotnet restore
dotnet run
```
3. API endpoints:
- GET /api/products
- GET /api/products/{id}
- POST /api/products (requires header X-API-KEY: inventory-secret)
- PUT /api/products/{id} (requires header X-API-KEY: inventory-secret)
- DELETE /api/products/{id} (requires header X-API-KEY: inventory-secret)

Responses are structured JSON: `{ success: bool, data: ..., count: ... }`
