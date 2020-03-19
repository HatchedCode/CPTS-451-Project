/*Business Portion of ER Model Begins*/
CREATE TABLE Business(
	rev_count INTEGER,
	businessID CHAR(22),
	busState VARCHAR,
	busName VARCHAR,
	longitude FLOAT,
	stars INTEGER,
	busAddress VARCHAR,
	busPostal VARCHAR,
	latitude FLOAT,
	busCity VARCHAR,
	is_Open INTEGER,
	PRIMARY KEY (businessID)
);

CREATE TABLE Hours(
	t_start VARCHAR,
	t_end VARCHAR,
	h_day VARCHAR,
	businessID CHAR(22),
	PRIMARY KEY (h_day, businessID),
	FOREIGN KEY (businessID) REFERENCES Business(businessID)
);

CREATE TABLE Attributes(
    att_name VARCHAR,
    value VARCHAR,
    businessID CHAR(22),
    PRIMARY KEY(att_name, businessID)
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
);

CREATE TABLE Category(
    cat_name VARCHAR,
    businessID VARCHAR(22),
    PRIMARY KEY(cat_name, businessID),
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
);
/*Business Portion of the ER Model Ends*/

/*Tip Portion of the ER Model Begins*/
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
/*Tip Portion of the ER Model Ends*/

/*User Portion of the ER Model Begins*/
CREATE TABLE User(
    user_id VARCHAR(50),
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

CREATE TABLE Friend(
	user_id VARCHAR(50),
	friend_user_id VARCHAR(50),
	PRIMARY KEY (user_id, friend_user_id),
	FOREIGN KEY (user_id) REFERENCES User (user_id)
	FOREIGN KEY (friend_id) REFERENCES User (friend_user_id)
);
/*User Portion of the ER Model Ends*/

/*Check_in Portion of the ER Model Begins*/
CREATE TABLE Check_in(
    day VARCHAR,
    time VARCHAR,
    year VARCHAR,
    month VARCHAR,
    businessID VARCHAR(22),
	user_id VARCHAR (50),
    PRIMARY KEY(day, time, year, month, businessID, user_id)
    FOREIGN KEY(businessID) REFERENCES Business(businessID)
	FOREIGN KEY(user_id) REFERENCES User(user_id)
);
/*Check_in Portion of the ER Model Begins*/
