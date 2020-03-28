/*UPDATE Statement for numCheckins Begins*/
UPDATE BusinessTable
SET numCheckins = checkCount.numCheckins
FROM (
    select businessID, COUNT(CheckInTable.businessID) as numCheckins
    from CheckInTable
    group by CheckInTable.businessID
) as checkCount
WHERE BusinessTable.businessID = checkCount.businessID;
/*UPDATE Statement for numCheckins Ends*/

/*UPDATE Statement for numTips Begins*/
UPDATE BusinessTable
SET numTips = tipCount.numTips
FROM (
    select TipTable.businessID, COUNT(TipTable.businessID) as numTips
    from TipTable
    group by TipTable.businessID
) as tipCount
WHERE BusinessTable.businessID = tipCount.businessID;
/*UPDATE Statement for numTips Ends*/

/*UPDATE Statement for totalLikes Begins*/
UPDATE UserTable
SET likecount = tips.tipLikes
FROM (
    select TipTable.user_id, SUM(TipTable.likes) as tipLikes
    from TipTable
    group by TipTable.user_id
) as tips
WHERE UserTable.user_id = tips.user_id;
/*UPDATE Statement for totalLikes Ends*/

/*UPDATE Statement for tipCount Begins*/
UPDATE UserTable
SET tipcount = tips.tiptotal
FROM (
    select TipTable.user_id, COUNT(TipTable.user_id) as tiptotal
    from TipTable
    group by TipTable.user_id
) as tips
WHERE UserTable.user_id = tips.user_id;
/*UPDATE Statement for tipCount Ends*/