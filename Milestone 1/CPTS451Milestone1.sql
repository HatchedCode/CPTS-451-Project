CREATE TABLE Business (
    businessID CHAR(22),
    businessName VARCHAR,
    businessAddr VARCHAR,
    businessState VARCHAR,
    businessCity VARCHAR,
    postal VARCHAR,
    latitude FLOAT,
    longitude FLOAT,
    businessStars FLOAT,
    is_open INTEGER,
    reviewCount INTEGER,
    att_name VARCHAR,-----attname-----
    cat_name VARCHAR,-----categories -----
    h_day INTEGER,-----hours 
    h_time INTEGER,-----hours 
    PRIMARY KEY (businessID),
    FOREIGN KEY(att_name, businessID) REFERENCES Attributes(att_name, businessID),
    FOREIGN KEY(cat_name, businessID) REFERENCES Category(cat_name, businessID),
    FOREIGN KEY(h_day, h_time, businessID) REFERENCES Attributes(h_day, h_time, businessID)
);

CREATE TABLE Tip (
    businessID CHAR(22),
    likes INTEGER,
    text VARCHAR,
    date VARCHAR,
    PRIMARY KEY (businessID, date, text)
    FOREIGN KEY (businessID) REFERENCES Business(businessID)
);

CREATE TABLE Check_in (
    day VARCHAR,
    time VARCHAR,
    year VARCHAR,
    month VARCHAR,
    businessID VARCHAR(22),
    PRIMARY KEY(day, time, year, month, businessID)
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
);

CREATE TABLE User (
    userID VARCHAR(50),
    name VARCHAR,
    funny INTEGER,
    yelping_since VARCHAR,
    useful INTEGER,
    fans INTEGER,
    cool INTEGER,
    average_stars FLOAT,
    tipcount INTEGER
    PRIMARY KEY (userID)
);

CREATE TABLE Attributes(
att_name VARCHAR,
value VARCHAR,
businessID VARCHAR(22),
PRIMARY KEY(att_name, businessID)
FOREIGN KEY(businessID) REFERENCES Business(businessID)
);

CREATE TABLE Hour(
h_day INTEGER,
h_time INTEGER,
businessID VARCHAR(22),
PRIMARY KEY(h_day, h_time, businessID),
FOREIGN KEY(businessID) REFERENCES Business(businessID)
);


CREATE TABLE Category(
cat_name VARCHAR,
businessID VARCHAR(22),
PRIMARY KEY(cat_name, businessID),
PRIMARY KEY(businessID) REFERENCES Business(businessID)
);

-----------------------------Relationships below-----------------------------------:
CREATE TABLE Friend(
user_id VARCHAR(50),
friend_id VARCHAR(50),
FOREIGN KEY(user_id) REFERENCES User(user_id),
FOREIGN KEY(friend_id) REFERENCES User(friend_id)
);

