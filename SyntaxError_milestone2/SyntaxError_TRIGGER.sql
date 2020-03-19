/*TRIGGER Statement for numTips Begins*/

/*TRIGGER Statement for numTips Ends*/

/*TRIGGER Statement for tipCount Begins*/

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
WHEN (OLD.businessID = NEW.businessID)
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

/*TRIGGER Statement for totalLikes Ends*/