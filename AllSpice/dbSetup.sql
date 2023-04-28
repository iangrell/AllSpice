-- Active: 1682439271856@@SG-sandbox-7500-mysql-master.servers.mongodirector.com@3306@Sandbox1

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture' createdAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    ) default charset utf8mb4 COMMENT '';

-- SECTION RECIPES

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        title VARCHAR(255) NOT NULL,
        instructions VARCHAR(4094) NOT NULL,
        img VARCHAR(1020) NOT NULL,
        category VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        'Arepas con Queso',
        'Arepas con Queso Method:

    In a large bowl, combine 12 ounces of water and salt.

    Slowly add the cornflour, in 4-5 additions, to the water mixture. Using your hands, begin mixing and kneading the dough until all of the cornflour is incorporated. Let the mixture sit for 3 minutes.

    Once the dough has rested, add the butter and cheese. Knead the dough for 4-5 minutes or until completely smooth and homogeneous. **try hand flattening one to check for proper moisture** If the dough is too dry, add another 1-2 ounces of water, or enough to form a supple but not sticky dough.

    Using wet hands, divide the dough into 8 relatively even balls. Press the balls into patties about ½ inch thick and 3-4 inches in diameter. 

    Preheat a large skillet over medium-low heat. Brush the pan with more softened butter, then add the arepas.

    Cook on each side for 5-7 minutes, or until browned on both sides and cooked through. The arepas should be fluffy but not wet when cut into.

    Optionally, allow the arepas to cool for 2-3 minutes, or until handleable. Then, slice an opening on one side of each arepa, making sure not to cut through to the other side. Place a folded 1-2 slices of mozzarella cheese in the opened pocket of each arepa. Return the arepas to the same pan and cook for 3-4 minutes, or until the cheese has melted.',
        'https://images.squarespace-cdn.com/content/v1/590be7fd15d5dbc6bf3e22d0/68394978-f5b9-4742-ae31-e984f3bc7f58/Screen+Shot+2022-01-11+at+2.25.35+PM.png?format=1000w',
        'Recipe',
        '642caa25c178a672b1f898d2'
    );

SELECT
    recipes.id,
    recipes.title,
    recipes.instructions,
    recipes.img,
    recipes.category,
    creator.name
FROM recipes
    JOIN accounts creator ON creator.id = recipes.creatorId
WHERE
    recipes.category = 'Recipe';

-- SECTION INGREDIENTS

CREATE TABLE
    ingredients(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

INSERT INTO
    ingredients(name, quantity, recipeId)
VALUES (
        'deli-sliced mozzarella cheese',
        'Optional: 8-16slices',
        '1'
    );

-- SECTION FAVORITES

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        UNIQUE(accountId, recipeId)
    ) default charset utf8mb4 COMMENT '';