CREATE TABLE Business(
    businessID CHAR(22),
    businessName VARCHAR,
    businessAddr VARCHAR,
    businessState VARCHAR,
    businessCity VARCHAR,
    postal VARCHAR,
    latitude FLOAT,
    longitude FLOAT,
    rev_count INTEGER, --Review count
    stars INTEGER,
    is_open INTEGER,
    PRIMARY KEY(businessID)
);

CREATE TABLE Tip(
    businessID CHAR(22),
    user_id VARCHAR(50),
    likes INTEGER,
    text VARCHAR,
    date DATE,
    PRIMARY KEY (businessID, date, user_id)
    FOREIGN KEY (businessID) REFERENCES Business(businessID)
    FOREIGN KEY (user_id) REFERENCES User(user_id)
);

CREATE TABLE Check_in(
    day VARCHAR,
    time VARCHAR,
    year VARCHAR,
    month VARCHAR,
    businessID VARCHAR(22),
    PRIMARY KEY(day, time, year, month, businessID)
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
);

CREATE TABLE User(
    userID VARCHAR(50),
    name VARCHAR,
    funny INTEGER,
    yelping_since VARCHAR,
    useful INTEGER,
    fans INTEGER,
    cool INTEGER,
    average_stars FLOAT,
    tipcount INTEGER,
    postcount INTEGER,
    likecount INTEGER,
    longitude FLOAT,
    latitude FLOAT,
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
    h_start_time TIME,
    h_end_time TIME,
    businessID VARCHAR(22),
    PRIMARY KEY(h_day, businessID),
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
);

CREATE TABLE Category(
    cat_name VARCHAR,
    businessID VARCHAR(22),
    PRIMARY KEY(cat_name, businessID),
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
);

-----------------------------Relationships below-----------------------------------:
CREATE TABLE Friend(
    user_id VARCHAR(50),
    friend_id VARCHAR(50),
    FOREIGN KEY(user_id) REFERENCES User(user_id),
    FOREIGN KEY(friend_id) REFERENCES User(friend_id)
);

