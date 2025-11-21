Article Marketplace Platform

This is my first full project built with ASP.NET. It is a simple online marketplace where users can upload their articles, set a price, and sell them. Other users can browse articles, purchase them, and leave comments or feedback.

🚀 Features
For Article Owners

Upload articles (PDF/text)

Set a price for each article

Manage published articles

View comments and feedback from buyers

For Buyers

Browse and search articles

View details and pricing

Purchase articles securely

Download purchased content

Leave comments or reviews

🛠️ Technologies Used

ASP.NET Core / MVC

Entity Framework Core

SQL Server

Identity Authentication

HTML, CSS, JavaScript

Optional: Bootstrap for UI (if used)

📦 Project Structure
/Controllers
/Models
/Views
/Services
/wwwroot
/appsettings.json

🔐 Authentication

The system uses ASP.NET Identity for:

User registration

Login / logout

Profile management

Different actions are restricted based on user roles (e.g., buyers vs. authors).

💳 Payment

If you included payment simulation:

Fake purchase logic for testing

Records stored in database

(You can later integrate Stripe or PayPal.)

📝 Comments & Reviews

After buying an article:

Users can write comments

Article owners can see all feedback

Helps maintain transparency and trust

🎯 Goal of the Project

This project helped me learn:

ASP.NET MVC structure

Database design with EF Core

Authentication & authorization

CRUD operations

Working with file uploads

Building a complete functional web app

📄 Future Improvements

Add real payment integration

Add rating system (stars)

Improve UI/UX

Add categories/tags for articles

Add admin panel
