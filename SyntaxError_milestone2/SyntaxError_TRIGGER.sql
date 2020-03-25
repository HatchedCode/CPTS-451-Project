/*TRIGGER Statement for numTips Begins*/
CREATE OR REPLACE FUNCTION updateNumTips() RETURNS trigger AS '
BEGIN
    UPDATE Business
    SET Business.numTips = tipCount.numTips
    FROM (
        select Tip.businessID, COUNT(Tip.businessID) as numTips
        from Tip
        group by Tip.businessID
    ) as tipCount
    WHERE Business.businessID = tipCount.businessID
    RETURN Business
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateNumTips
AFTER INSERT ON Business
FOR EACH ROW
WHEN (old.businessID = new.businessID)
EXECUTE PROCEDURE updateNumTips();
/*TRIGGER Statement for numTips Ends*/

/*TRIGGER Statement for tipCount Begins*/
CREATE OR REPLACE FUNCTION updateTipCount() RETURNS trigger AS '
BEGIN
    UPDATE User
    SET User.tipcount = tips.tipcount
    FROM (
        select Tip.user_id, COUNT(Tip.user_id) as tipcount
        from Tip
        group by Tip.user_id
    ) as tips
    WHERE User.user_id = tips.user_id
    RETURN User
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateTipCount
AFTER INSERT ON User
FOR EACH ROW
WHEN (old.user_id = new.user_id)
EXECUTE PROCEDURE updateTipCount();
/*TRIGGER Statement for tipCount Ends*/

/*TRIGGER Statement for numCheckins Begins*/
CREATE OR REPLACE FUNCTION updateNumCheckins() RETURNS trigger AS '
BEGIN 
    UPDATE Business
    SET Business.numCheckins = checkCount.numCheckins
    FROM (
        select Check_in.businessID, COUNT(Check_in.businessID) as numCheckins
        FROM Check_in
        WHERE Check_in.businessID = NEW.businessID
        GROUP BY Check_in.businessID
    ) as checkCount
    WHERE Business.businessID = checkCount.businessID
   RETURN NEW;
END
' LANGUAGE plpgsql; 

CREATE TRIGGER updateCheckins
AFTER INSERT ON Check_in
FOR EACH ROW 
WHEN (OLD.businessID = NEW.businessID) --Remove this line and just check that NEW is not null, if it does not work.
EXECUTE PROCEDURE updateNumCheckins();


-- numCheckin Trigger TESTS are below--
--Test 1
INSERT INTO Check_in
VALUES('20', '21:16:27', '2020', '04', '-000aQFeK6tqVLndf7xORg', '09owAly0xUSt_JlDVLuNJg');
SELECT * FROM Check_in ORDER BY user_id

--Clean Test 1
DELETE FROM Check_in
WHERE user_id = '09owAly0xUSt_JlDVLuNJg' AND businessID = '-000aQFeK6tqVLndf7xORg' AND
day = '20' AND month = '04' AND time = '21:16:27' AND year = '2020'

DROP TRIGGER updateCheckins on Check_in 
/*TRIGGER Statement for numCheckins Ends*/

/*TRIGGER Statement for totalLikes Begins*/
CREATE OR REPLACE FUNCTION updateTotalLikes() RETURNS trigger AS '
BEGIN
    UPDATE User
    SET User.totalLikes = totalLikes.likes
    FROM (
        SELECT Tip.user_id, Tip.likes
        FROM Tip
        GROUP BY Tip.user_id
    ) as totalLikes
    WHERE User.user_id = likes.user_id
    RETURN User
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateTotalLikes
AFTER INSERT ON User
FOR EACH ROW
WHEN (OLD.user_id = NEW.user_id)
EXECUTE PROCEDURE updateTotalLikes();
/*TRIGGER Statement for totalLikes Ends*/