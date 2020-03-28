/*BusinessTable Portion of ER Model Begins*/
CREATE TABLE BusinessTable(
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
    numTips INTEGER,
    numCheckins INTEGER,
	PRIMARY KEY (businessID)
);

CREATE TABLE HoursTable(
	t_start VARCHAR,
	t_end VARCHAR,
	h_day VARCHAR,
	businessID CHAR(22),
	PRIMARY KEY (h_day, businessID),
	FOREIGN KEY (businessID) REFERENCES BusinessTable(businessID)
);

CREATE TABLE AttributesTable(
    att_name VARCHAR,
    value VARCHAR,
    businessID CHAR(22),
    PRIMARY KEY(att_name, businessID),
    FOREIGN KEY(businessID) REFERENCES BusinessTable(businessID)
);

CREATE TABLE CategoryTable(
    cat_name VARCHAR,
    businessID VARCHAR(22),
    PRIMARY KEY(cat_name, businessID),
    FOREIGN KEY(businessID) REFERENCES BusinessTable(businessID)
);
/*BusinessTable Portion of the ER Model Ends*/


/*UserTable Portion of the ER Model Begins*/
CREATE TABLE UserTable(
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
    PRIMARY KEY(user_id)
);

/*TipTable Portion of the ER Model Begins*/
CREATE TABLE TipTable(
    businessID CHAR(22),
    user_id VARCHAR(50),
    likes INTEGER,
    text VARCHAR,
    date timestamp,
    PRIMARY KEY(businessID, date, user_id),
    FOREIGN KEY(businessID) REFERENCES BusinessTable(businessID),
    FOREIGN KEY(user_id) REFERENCES UserTable(user_id)
);
/*TipTable Portion of the ER Model Ends*/

CREATE TABLE FriendTable(
	current_user_id VARCHAR(50),
	friend_user_id VARCHAR(50),
	PRIMARY KEY(current_user_id, friend_user_id),
	FOREIGN KEY(current_user_id) REFERENCES UserTable(user_id),
	FOREIGN KEY(friend_user_id) REFERENCES UserTable(user_id)
);
/*UserTable Portion of the ER Model Ends*/

/*CheckInTable Portion of the ER Model Begins*/
CREATE TABLE CheckInTable(
    day VARCHAR,
    time VARCHAR,
    year VARCHAR,
    month VARCHAR,
    businessID VARCHAR(22),
    PRIMARY KEY(day, time, year, month, businessID),
    FOREIGN KEY(businessID) REFERENCES BusinessTable(businessID)
);
/*CheckInTable Portion of the ER Model Ends*/