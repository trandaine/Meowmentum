add-migration AddCurrencyNavigation -context EnglishAppDbContext -outputdir Data/Migrations
add-migration UpdateCustomerPaymentRecord -context EnglishAppDbContext -outputdir Data/Migrations


add-migration Add_table_CartItem_and_ShoppingCart -context EnglishAppDbContext -outputdir Data/Migrations
add-migration Add_Subject_entity_to_CourseComments -context EnglishAppDbContext -outputdir Data/Migrations
add-migration Add_Configurations_to_CourseComments -context EnglishAppDbContext -outputdir Data/Migrations
add-migration Add_Name_to_CartItem -context EnglishAppDbContext -outputdir Data/Migrations

add-migration InitializeDb -context EnglishAppIdentityDbContext -outputdir Data/IdentityMigrations
add-migration FixIdentityUser -context EnglishAppIdentityDbContext -outputdir Data/IdentityMigrations

update-database -context EnglishAppDbContext

update-database -context EnglishAppIdentityDbContext

remove-migration -context EnglishAppDbContext