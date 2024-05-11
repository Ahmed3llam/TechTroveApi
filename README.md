# TechTrove E-Commerce Website API Redmi File
## Authentication Endpoints
POST /api/auth/login: Endpoint for user login. Requires email and password. Returns token and user identity.
POST /api/auth/register: Endpoint for user registration. Requires user details. Returns token and user identity.
POST /api/auth/admin/register: Endpoint for admin registration. Requires admin details. Returns token and admin identity.
## User Endpoints
GET /api/users/profile: Retrieves user profile data. Requires authentication token.
PUT /api/users/update: Updates user profile data. Requires authentication token and updated data.
PUT /api/users/image: Updates user profile image. Requires authentication token and image file.
PUT /api/users/password: Updates user password. Requires authentication token and new password.
POST /api/users/logout: Logs out user. Requires authentication token.
DELETE /api/users/delete: Deletes user account. Requires authentication token.
## Product Endpoints
GET /api/products?page={page}&pageSize={pageSize}: Retrieves paginated list of products.
GET /api/products/{id}: Retrieves product details by ID.
PUT /api/products/update/{id}: Updates product data. Requires product ID and updated data.
PUT /api/products/image/{id}: Updates product image. Requires product ID and image file.
DELETE /api/products/delete/{id}: Deletes product by ID.
## Category Endpoints
GET /api/categories?page={page}&pageSize={pageSize}: Retrieves paginated list of categories.
GET /api/categories/{id}: Retrieves category details by ID.
PUT /api/categories/update/{id}: Updates category data. Requires category ID and updated data.
DELETE /api/categories/delete/{id}: Deletes category by ID.
## Cart Endpoints
GET /api/cart: Retrieves cart items for logged-in user.
POST /api/cart/add: Adds item to cart. Requires authentication token and product ID.
PUT /api/cart/update/{productId}: Updates quantity of item in cart. Requires authentication token, product ID, and new quantity.
DELETE /api/cart/delete/{productId}: Deletes item from cart. Requires authentication token and product ID.
Pagination
Pagination is implemented for both products and categories endpoints using page and pageSize query parameters.
Filtering
Products can be filtered based on a list of categories using appropriate query parameters.
Security
Authentication tokens are required for accessing user-specific endpoints and actions.
