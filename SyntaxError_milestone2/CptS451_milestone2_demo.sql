
-- --GLOSSARY
-- --table names
-- BusinessTable
-- UserTable
-- tipstable
-- friendstable
-- CheckInTable
-- CategoryTable
-- AttributesTable
-- businesshours

-- --some att_name names
-- busPostal
-- businessID
-- busCity  (business busCity)
-- name   (business name)
-- user_id
-- friend_user_id
-- numTips
-- numCheckins

-- user_id
-- tipcount  (user)
-- likecount (user)

-- tipdate
-- tiptext
-- likes  (tip)

-- checkinyear
-- checkinmonth
-- checkinday
-- checkintime


--1.
SELECT COUNT(*) 
FROM  BusinessTable;
SELECT COUNT(*) 
FROM  UserTable;
SELECT COUNT(*) 
FROM  TipTable;
SELECT COUNT(*) 
FROM  FriendTable;
SELECT COUNT(*) 
FROM  CheckInTable;
SELECT COUNT(*) 
FROM  CategoryTable;
SELECT COUNT(*) 
FROM  AttributesTable;
SELECT COUNT(*) 
FROM  HoursTable;



--2. Run the following queries on your business table, CheckInTable table and review table. Make sure to change the att_name names based on your schema. 

SELECT busPostal, count(businessID) 
FROM BusinessTable
GROUP BY busPostal
HAVING count(businessID) > 500
ORDER BY busPostal;

SELECT busPostal, COUNT(distinct C.cat_name)
FROM BusinessTable as B, CategoryTable as C
WHERE B.businessID = C.businessID
GROUP BY busPostal
HAVING count(distinct C.cat_name)>300
ORDER BY busPostal;

SELECT busPostal, COUNT(distinct A.att_name)
FROM BusinessTable as B, AttributesTable as A
WHERE B.businessID = A.businessID
GROUP BY busPostal
HAVING count(distinct A.att_name)>65;


--3. Run the following queries on your business table, CheckInTable table and tips table. Make sure to change the att_name names based on your schema. 

SELECT UserTable.user_id, count(friend_user_id)
FROM UserTable, friendstable
WHERE UserTable.user_id = friendstable.user_id AND 
      UserTable.user_id = 'zvQ7B3KZuFOX7pYLsOxhpA'
GROUP BY UserTable.user_id;


SELECT businessID, busName, busCity, numTips, numCheckins 
FROM BusinessTable 
WHERE businessID ='UvF68aNDfzCWQbxO6-647g' ;

SELECT user_id, name, tipcount, likecount
FROM UserTable
WHERE user_id = 'i3bLA4sEdFk8j3Pq6tx8wQ'

-----------

SELECT COUNT(*) 
FROM CheckInTable
WHERE businessID ='UvF68aNDfzCWQbxO6-647g';

SELECT count(*)
FROM tipstable
WHERE  businessID = 'UvF68aNDfzCWQbxO6-647g';



--4. 
--Type the following statements. Make sure to change the att_name names based on your schema. 

SELECT COUNT(*) 
FROM CheckInTable
WHERE businessID ='M007_bAIM34x1yd138zhSQ';

SELECT businessID,busName, busCity, numCheckins, numTips
FROM BusinessTable 
WHERE businessID ='M007_bAIM34x1yd138zhSQ';

INSERT INTO CheckInTable (businessID, year,month, day,time)
VALUES ('M007_bAIM34x1yd138zhSQ',’2020’,’03’,'27','15:00');


--5.
--Type the following statements. Make sure to change the att_name names based on your schema.  

SELECT businessID,busName, busCity, numCheckins, numTips
FROM BusinessTable 
WHERE businessID ='M007_bAIM34x1yd138zhSQ';

SELECT user_id, name, tipcount, likecount
FROM UserTable
WHERE user_id = 'rRrFcSEZOTw6iZagsIwTFQ'


INSERT INTO tipstable (user_id, businessID, tipdate, tiptext,likes)  
VALUES ('rRrFcSEZOTw6iZagsIwTFQ','M007_bAIM34x1yd138zhSQ', '2020-03-27 13:00','EVERYTHING IS AWESOME',0);

UPDATE tipstable 
SET likes = likes+1
WHERE user_id = 'rRrFcSEZOTw6iZagsIwTFQ' AND 
      businessID = 'M007_bAIM34x1yd138zhSQ' AND 
      tipdate ='2020-03-27 13:00'
      