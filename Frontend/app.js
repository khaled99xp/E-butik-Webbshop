// API Base URL
const API_BASE_URL = 'http://localhost:5001';

// Helper function to convert URL query string to an object
function getQueryParams() {
  const params = {};
  window.location.search.substring(1).split("&").forEach(pair => {
    if(pair) {
      const [key, value] = pair.split("=");
      params[key] = decodeURIComponent(value);
    }
  });
  return params;
}

/* =====================
   User Registration (Register)
   ===================== */
if (document.getElementById('registerForm')) {
  document.getElementById('registerForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value.trim();

    const response = await fetch(`${API_BASE_URL}/register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name, email, password })
    });
    const data = await response.json();
    const messageEl = document.getElementById('registerMessage');
    if (response.ok) {
      messageEl.innerHTML = `<div class="alert alert-success">${data.message}</div>`;
      setTimeout(() => window.location.href = 'login.html', 2000);
    } else {
      messageEl.innerHTML = `<div class="alert alert-danger">${data.message}</div>`;
    }
  });
}

/* =====================
   User Login
   ===================== */
   if (document.getElementById('loginForm')) {
    document.getElementById('loginForm').addEventListener('submit', async function(e) {
      e.preventDefault();
      const email = document.getElementById('loginEmail').value.trim();
      const password = document.getElementById('loginPassword').value.trim();
  
      const response = await fetch(`${API_BASE_URL}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
      });
      const data = await response.json();
      console.log("Token returned:", data.token); 
      const messageEl = document.getElementById('loginMessage');
      if (response.ok) {
        localStorage.setItem('authToken', data.token);
        messageEl.innerHTML = `<div class="alert alert-success">Login successful</div>`;

        const token = data.token.trim();

        if(token === "1234test1234") {
          setTimeout(() => window.location.href = 'admin.html', 2000);
        } else {
          setTimeout(() => window.location.href = 'index.html', 2000);
        }
      } else {
        messageEl.innerHTML = `<div class="alert alert-danger">${data.message}</div>`;
      }
    });
  }
  

/* =====================
   Load Products on Home Page
   ===================== */
if (document.getElementById('productsContainer')) {
  async function loadProducts() {
    const response = await fetch(`${API_BASE_URL}/api/products`);
    const products = await response.json();
    const container = document.getElementById('productsContainer');
    container.innerHTML = '';
    products.forEach(product => {
      // product.imageUrl placeholder
      const imageUrl = product.imageUrl ? product.imageUrl : './img/dummy.png';
      container.innerHTML += `
        <div class="col-md-4">
          <div class="card">
            <img src="${imageUrl}" class="card-img-top" alt="${product.name}">
            <div class="card-body">
              <h5 class="card-title">${product.name}</h5>
              <p class="card-text">Price: ${product.price} Kr</p>
              <a href="product.html?id=${product.id}" class="btn btn-primary">View Details</a>
            </div>
          </div>
        </div>
      `;
    });
  }
  loadProducts();
}

/* =====================
   Load Product Details on product.html
   ===================== */
if (document.getElementById('productDetailsContainer')) {
  async function loadProductDetails() {
    const params = getQueryParams();
    const productId = params.id;
    if (!productId) {
      document.getElementById('productDetailsContainer').innerHTML = '<p>Product ID not specified</p>';
      return;
    }
    const response = await fetch(`${API_BASE_URL}/api/products/${productId}`);
    if (!response.ok) {
      document.getElementById('productDetailsContainer').innerHTML = '<p>Product not found</p>';
      return;
    }
    const product = await response.json();
    // product.imageUrl placeholder
    const imageUrl = product.imageUrl ? product.imageUrl : './img/dummy.png';
    const container = document.getElementById('productDetailsContainer');
    container.innerHTML = `
      <div class="row">
        <div class="col-md-6">
          <img src="${imageUrl}" alt="${product.name}" class="img-fluid">
        </div>
        <div class="col-md-6">
          <h2>${product.name}</h2>
          <p class="text-muted">Category: ${product.category ? product.category.name : 'Unspecified'}</p>
          <h3 class="text-success">Price: ${product.price} Kr</h3>
          <p>${product.description}</p>
          <p>Stock: ${product.stock}</p>
          <button class="btn btn-warning btn-lg">Add to Cart</button>
        </div>
      </div>
    `;
  }
  loadProductDetails();
}
