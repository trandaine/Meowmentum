# TechStack & Solution Documentation

This document serves as a comprehensive guide to the **EnglishApp** solution, detailing the technology stack, project structure, and in-depth analysis of the API, MVC Controllers, and Business Logic Interfaces.

---

## 🚀 1. Technology Stack

### Backend & Core
-   **Framework**: .NET 10.0
-   **Web Frameworks**: ASP.NET Core MVC, ASP.NET Core Web API
-   **ORM**: Entity Framework Core 10.0.0
-   **Database**: SQL Server
-   **Authentication**: ASP.NET Core Identity
-   **Object Mapping**: (Implicit/Manual or AutoMapper if used - *Note: DTOs are present*)
-   **Dependency Injection**: Native ASP.NET Core DI

### Frontend (MVC)
-   **View Engine**: Razor Views
-   **Styling**: CSS (Bootstrap/Custom), Responsive Design
-   **Libraries**:
    -   jQuery (implied by standard MVC templates)
    -   CKEditor / TinyMCE (for rich text editing)
    -   mmenu (for mobile menus)

### Infrastructure & Tools
-   **Containerization**: Docker support (Linux containers)
-   **API Documentation**: Swagger / OpenAPI
-   **Testing**: FluentAssertions
-   **Version Control**: Git

---

## 📂 2. Project Structure

The solution follows a Clean Architecture / Layered approach:

-   **`EnglishApp.API`**: The RESTful API entry point. Exposes endpoints for external consumers or mobile apps. Includes Swagger documentation.
-   **`EnglishApp.MVC`**: The main web application entry point. Handles user interactions, views, and controllers.
-   **`EnglishApp.ApplicationCore`**: The core of the application. Contains:
    -   **Entities**: Database models (Courses, Lessons, etc.).
    -   **IdentityEntities**: Extended Identity users and roles.
    -   **Enums**: Enumerations for status, types, etc.
    -   **Constants**: System-wide constants.
-   **`EnglishApp.BusinessLogic`**: Contains the business rules and logic.
    -   **Services**: Implementation of business logic (CoursesService, CartService, etc.).
    -   **DTOs**: Data Transfer Objects for passing data between layers.
    -   **Interfaces**: Service contracts.
-   **`EnglishApp.Infrastructure`**: Handles data access and external concerns.
    -   **Data**: `DbContext` configurations (`EnglishAppDbContext`, `EnglishAppIdentityDbContext`).
    -   **Services**: Implementation of infrastructure-related services.
    -   **Migrations**: Database migrations.
-   **`EnglishApp.Common`**: Shared utility classes and extensions used across the solution.
-   **`SharedMedia`**: A directory outside the project root used for storing and serving uploaded media files.

---

## 🎮 3. MVC Controllers Overview

Detailed explanation of each controller in the `EnglishApp.MVC` project.

### 3.1 AuthenticationController
**Purpose**: Handles user authentication and account management (Login, Register, Logout).

*   **Dependencies**:
    *   `SignInManager<EnglishAppIdentityUser>`: For signing users in and out.
    *   `UserManager<EnglishAppIdentityUser>`: For managing user accounts (creation, finding).
    *   `EnglishAppDbContext`: To check for existing Customer profiles.
*   **Key Actions**:
    *   `Login` (GET/POST): Authenticates a user. If successful, checks if a `Customer` profile exists. If not, redirects to `Customers/Create`.
    *   `Register` (GET/POST): Creates a new `EnglishAppIdentityUser`. Upon success, signs the user in and redirects to Login (or Home).
    *   `Logout`: Signs the user out and redirects to Login.

### 3.2 CartController
**Purpose**: Manages shopping cart operations, primarily designed for AJAX interactions (returning `Ok` or `BadRequest`).

*   **Dependencies**:
    *   `ICartService`: Defines operations for managing the shopping cart items directly.
    *   `ICustomerService`: Defines operations for managing customer profiles and current user context.
    *   `EnglishAppDbContext`: Direct database access (though mostly commented out in favor of service).
*   **Key Actions**:
    *   `AddToCart` (POST): Adds a course to the cart. Uses `_cartService`.
    *   `IncreaseItemQuantity` / `DecreaseItemQuantity`: Adjusts quantities of items in the cart.
    *   `Delete` (POST): Removes an item from the cart.
*   **Note**: This controller seems to be the "modern" or "refactored" version using `ICartService` compared to `ShoppingCartsController`.

### 3.3 CommentsController
**Purpose**: Allows users to post comments on courses.

*   **Dependencies**:
    *   `ICommentsService`: Defines operations for managing course comments.
    *   `ICustomerService`: Defines operations for managing customer profiles and current user context.
*   **Key Actions**:
    *   `Details`: Views a specific comment (Admin/Debug purpose likely).
    *   `Create` (GET): Returns a partial view to post a comment.
    *   `Create` (POST): Submits a new comment via `ICommentsService`. Returns JSON on success for AJAX handling.

### 3.4 CoursesController
**Purpose**: Admin-facing controller for managing the catalog of courses.

*   **Dependencies**:
    *   `ICoursesService`: Defines operations for managing courses.
    *   `IWebHostEnvironment`: For accessing web root path (used in image uploads).
*   **Key Actions**:
    *   `Index`: Lists all courses (Admin view).
    *   `Details`: Shows details of a specific course.
    *   `Create` / `Edit` (GET/POST): Handles course creation and updates, including thumbnail image upload using `MediaHelper`.
    *   `Delete`: Removes a course.
    *   `LessonLists`: Returns a ViewComponent for the list of lessons (used for dynamic updates).

### 3.5 CustomersController
**Purpose**: Manages customer profiles.

*   **Dependencies**:
    *   `ICustomerService`: Defines operations for managing customer profiles and current user context.
    *   `IWebHostEnvironment`: For avatar image uploads.
*   **Key Actions**:
    *   `Details`: Shows the current user's profile.
    *   `Create` (GET/POST): Creates a new Customer profile (usually after first registration).
    *   `Edit` (GET/POST): Updates customer profile, including avatar upload.

### 3.6 HomeController
**Purpose**: The main public-facing controller for the storefront.

*   **Dependencies**:
    *   `ICoursesService`: Defines operations for retrieving course information.
*   **Key Actions**:
    *   `Index`: Displays the list of courses available for purchase.
    *   `CourseDetails`: Shows detailed information about a specific course for customers.

### 3.7 LessonsController
**Purpose**: Manages lessons associated with courses.

*   **Dependencies**:
    *   `LessonService`: Logic for lesson management.
    *   `EnglishAppDbContext`: Direct context usage for some operations (could be refactored to service).
*   **Key Actions**:
    *   `Index` / `Details`: Lists or shows lesson details.
    *   `Create` / `Edit` (GET/POST): Manages lesson content (Video, Audio, Text). Handles AJAX form submissions returning JSON.
    *   `Delete`: Removes a lesson.
    *   `LessonComponentList`: Returns the `LessonList` ViewComponent (similar to `CoursesController`).

### 3.8 ShoppingCartsController
**Purpose**: An alternative or legacy controller for shopping cart management, utilizing Session state.

*   **Dependencies**:
    *   `IShoppingCartService`: Defines operations for retrieving shopping cart state and summaries.
    *   `ISession`: Directly manipulates HTTP Session.
*   **Key Actions**:
    *   `Index` / `Details`: Views the cart.
    *   `AddToCart` (POST): Adds items to a Session-based cart (`KEY_CART`).
    *   `GetShoppingCartItemListComponentView` / `GetCartQuantityComponentView`: Returns ViewComponents to update the UI (cart icon count, cart list dropdown).
*   **Observation**: There is an overlap between `CartController` and `ShoppingCartsController`. `CartController` appears to be database-backed via `ICartService`, while `ShoppingCartsController` relies heavily on Session and `IShoppingCartService`.

---

## 📡 4. API Controllers Overview

Detailed explanation of each controller in the `EnglishApp.API` project.

### 4.1 CoursesController
**Purpose**: Provides RESTful endpoints for accessing course data.

*   **Dependencies**:
    *   `ICoursesService`: Business logic for retrieving course information.
*   **Key Actions**:
    *   `Get` (GET `api/Courses`): Retrieves a list of all courses. Returns a collection of `CourseDTO`.
    *   `Get(id)` (GET `api/Courses/{id}`): Retrieves details of a specific course by its ID. Returns a `CourseDTO`.
    *   `Post`, `Put`, `Delete`: Currently defined but not implemented (placeholders).

---

## 🧠 5. BusinessLogic Interfaces Overview

Detailed explanation of each interface in the `EnglishApp.BusinessLogic` project. These interfaces define the contracts for the service layer.

### 5.1 ICartService
**Purpose**: Defines operations for managing the shopping cart items directly.

*   **Methods**:
    *   `AddToCart(int productId, int quantity = 1)`: Adds a specified quantity of a product (course) to the cart. Returns a `StatusCode`.
    *   `Delete(int cartItemId)`: Removes an item from the cart by its ID.
    *   `Increase(int cartItemId)`: Increases the quantity of a specific cart item by 1.
    *   `Decrease(int cartItemId)`: Decreases the quantity of a specific cart item by 1.

### 5.2 ICommentsService
**Purpose**: Defines operations for managing course comments.

*   **Methods**:
    *   `GetCourseCommentDtoById(int idCourse)`: Retrieves all comments for a specific course. Returns an array of `CourseCommentDTO`.
    *   `Create(CourseCommentDTO courseCommentDTO)`: Adds a new comment to a course. Returns a `StatusCode`.

### 5.3 ICoursesService
**Purpose**: Defines the core CRUD operations for managing courses.

*   **Methods**:
    *   `GetCourseDtoById(int idCourse)`: Retrieves a single course by its ID. Returns a `CourseDTO` or null.
    *   `GetAllCourseDto()`: Retrieves all available courses. Returns an array of `CourseDTO`.
    *   `Create(CourseDTO courseModel)`: Creates a new course. Returns a `StatusCode`.
    *   `Update(CourseDTO courseModel)`: Updates an existing course. Returns a `StatusCode`.
    *   `Delete(int courseId)`: Deletes a course by its ID. Returns a `StatusCode`.

### 5.4 ICustomerService
**Purpose**: Defines operations for managing customer profiles and current user context.

*   **Methods**:
    *   `GetCurrentCustomerName()`: Retrieves the name of the currently logged-in customer.
    *   `GetCurrentUserId()`: Retrieves the User ID (GUID string) of the currently logged-in user.
    *   `Create(CustomerDTO customerDto)`: Creates a new customer profile.
    *   `Update(CustomerDTO customerDto)`: Updates an existing customer profile.
    *   `GetCustomerDtoByUserId(string userId)`: Retrieves customer details using the Identity User ID.
    *   `GetCustomerDtoById(int id)`: Retrieves customer details using the Customer ID (int).
    *   `CustomerExists(int id)`: Checks if a customer exists by ID.

### 5.5 IShoppingCartService
**Purpose**: Defines operations for retrieving shopping cart state and summaries.

*   **Methods**:
    *   `GetCountItemsInShoppingCart()`: Retrieves the total number of items in the current user's cart. Returns a string (likely for UI display).
    *   `GetCartItemsByUserId(string userId)`: Retrieves all cart items for a specific user. Returns an array of `CartItemDTO`.


