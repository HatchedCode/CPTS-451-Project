/*TRIGGER Statement for numTips Begins*/
CREATE OR REPLACE FUNCTION updateNumTips() RETURNS trigger AS '
BEGIN
    UPDATE BusinessTable
    SET numTips = tipCount.numTips
    FROM (
        select TipTable.businessID, COUNT(TipTable.businessID) as numTips
        from TipTable
        group by TipTable.businessID
    ) as tipCount
    WHERE BusinessTable.businessID = tipCount.businessID;
    RETURN new;
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateNumTips
AFTER INSERT ON TipTable
FOR EACH ROW
WHEN (new.businessID is not null)
EXECUTE PROCEDURE updateNumTips();
/TRIGGER Statement for numTips Ends/

--Test 1
INSERT INTO TipTable (businessID, user_id, likes, text, date) 
VALUES ('5KheTjYPu1HcQzQFtm4_vw','jRyO2V1pA4CdVVqCIOPc1Q','32','Testing 1 2 3','2020-12-26');

SELECT * FROM BusinessTable 
WHERE businessID = '5KheTjYPu1HcQzQFtm4_vw';

--Clean Test 1
DELETE FROM TipTable
WHERE businessID = '5KheTjYPu1HcQzQFtm4_vw' AND user_id = 'jRyO2V1pA4CdVVqCIOPc1Q' AND date = '2020-12-26';

DROP TRIGGER updateNumTips on TipTable
/*TRIGGER Statement for numTips Ends*/

/*TRIGGER Statement for tipCount Begins*/
CREATE OR REPLACE FUNCTION updateNewTipCount() RETURNS trigger AS '
BEGIN
    UPDATE UserTable
    SET tipcount = tips.t_tipcount
    FROM (
        select TipTable.user_id, COUNT(TipTable.user_id) as t_tipcount
        from TipTable
        group by TipTable.user_id
    ) as tips
    WHERE UserTable.user_id = tips.user_id;
    RETURN NEW;
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateTipCount
AFTER INSERT ON TipTable
FOR EACH ROW
WHEN (NEW.user_id is not NULL AND NEW.businessID is not NULL)
EXECUTE PROCEDURE updateNewTipCount();


--test1 Begins
SELECT * FROM UserTable
WHERE UserTable.user_id = 'jRyO2V1pA4CdVVqCIOPc1Q'
ORDER BY user_id;

INSERT INTO TipTable (businessID, user_id, likes, text, date) 
VALUES ('5KheTjYPu1HcQzQFtm4_vw','jRyO2V1pA4CdVVqCIOPc1Q','40','I love CHIPS AND SALSA!!!!!!!!!!!!!!!!!!!!!!!','2020-12-30');



SELECT COUNT(*) FROM TipTable
WHERE TipTable.user_id = 'jRyO2V1pA4CdVVqCIOPc1Q'

SELECT * FROM UserTable
WHERE UserTable.user_id = 'jRyO2V1pA4CdVVqCIOPc1Q'
ORDER BY user_id;


--Clean Test 1
DELETE FROM TipTable
WHERE TipTable.user_id = 'jRyO2V1pA4CdVVqCIOPc1Q' AND TipTable.businessID = '5KheTjYPu1HcQzQFtm4_vw' AND date = '2020-12-30';

DROP TRIGGER updateTipCount on TipTable 

/*TRIGGER Statement for tipCount Ends*/

/*TRIGGER Statement for numCheckins Begins*/
CREATE OR REPLACE FUNCTION updateNumCheckins() RETURNS trigger AS '
BEGIN 
    UPDATE BusinessTable
    SET numCheckins = checkCount.totalCheckin
    FROM (
        select CheckInTable.businessID, COUNT(CheckInTable.businessID) as totalCheckin
        FROM CheckInTable
        WHERE CheckInTable.businessID = NEW.businessID
        GROUP BY CheckInTable.businessID
    ) as checkCount
    WHERE BusinessTable.businessID = checkCount.businessID;
   RETURN NEW;
END
' LANGUAGE plpgsql; 

CREATE TRIGGER updateCheckins
AFTER INSERT ON CheckInTable
FOR EACH ROW 
WHEN (NEW.businessID is not NULL) --Remove this line and just check that NEW is not null, if it does not work.
EXECUTE PROCEDURE updateNumCheckins();


-- numCheckin Trigger TESTS are below--
--Test 1
SELECT * FROM BusinessTable
WHERE BusinessTable.businessID = '-000aQFeK6tqVLndf7xORg'
ORDER BY businessID;

INSERT INTO CheckInTable
VALUES('20', '22:11:28', '2020', '04', '-000aQFeK6tqVLndf7xORg');
SELECT * FROM CheckInTable 
WHERE CheckInTable.businessID = '-000aQFeK6tqVLndf7xORg'
ORDER BY businessID;

SELECT * FROM BusinessTable
WHERE BusinessTable.businessID = '-000aQFeK6tqVLndf7xORg'
ORDER BY businessID;


--Clean Test 1
DELETE FROM CheckInTable
WHERE businessID = '-000aQFeK6tqVLndf7xORg' AND
day = '20' AND month = '04' AND time = '21:16:27' AND year = '2020';

DROP TRIGGER updateCheckins on CheckInTable 
/*TRIGGER Statement for numCheckins Ends*/

/*TRIGGER Statement for totalLikes Begins*/
CREATE OR REPLACE FUNCTION updateTotalLikes() RETURNS trigger AS '
BEGIN
    UPDATE UserTable
    SET likecount = tips.tipLikes
    FROM (
        select TipTable.user_id, SUM(TipTable.likes) as tipLikes
        from TipTable
        group by TipTable.user_id
    ) as tips
    WHERE UserTable.user_id = tips.user_id;
    RETURN NEW;
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateTotalLikes
AFTER INSERT ON TipTable
FOR EACH ROW
WHEN (NEW.user_id is not null) 
EXECUTE PROCEDURE updateTotalLikes();

-- totalLikes Trigger TESTS are below--
--Test 1
INSERT INTO TipTable
VALUES('D2nfOrnJ2OBlX_428sKyMg','3KkT6SmPFLGvBS1pnDBr8g','100','Short lines at lunch!!','2020-11-28');

SELECT * FROM UserTable 
WHERE user_id = '3KkT6SmPFLGvBS1pnDBr8g';

--Clean Test 1
DELETE FROM TipTable
WHERE user_id = '3KkT6SmPFLGvBS1pnDBr8g' AND businessID = 'D2nfOrnJ2OBlX_428sKyMg' AND
date = '2020-11-28'

DROP TRIGGER updateTotalLikes on TipTable
/*TRIGGER Statement for totalLikes Ends*/