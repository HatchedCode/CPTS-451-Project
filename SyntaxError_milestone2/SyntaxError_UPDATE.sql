/*UPDATE Statement for numCheckins Begins*/
UPDATE Business
SET Business.numCheckins = checkCount.numCheckins
FROM (
    select Check_in.businessID, COUNT(Check_in.businessID) as numCheckins
    from Check_in
    group by Check_in.businessID
) as checkCount
WHERE Business.businessID = checkCount.businessID 
/*UPDATE Statement for numCheckins Ends*/

/*UPDATE Statement for numTips Begins*/
UPDATE Business
SET Business.numTips = tipCount.numTips
FROM (
    select Tip.businessID, COUNT(Tip.businessID) as numTips
    from Tip
    group by Tip.businessID
) as tipCount
WHERE Business.businessID = tipCount.businessID 
/*UPDATE Statement for numTips Ends*/

/*UPDATE Statement for totalLikes Begins*/
UPDATE User
SET User.likecount = tips.tipLikes
FROM (
    select Tip.user_id, SUM(Tip.likes) as tipLikes
    from Tip
    group by Tip.user_id
) as tips
WHERE User.user_id = tips.user_id
/*UPDATE Statement for totalLikes Ends*/

/*UPDATE Statement for tipCount Begins*/
UPDATE User
SET User.tipcount = tips.tipcount
FROM (
    select Tip.user_id, COUNT(Tip.user_id) as tipcount
    from Tip
    group by Tip.user_id
) as tips
WHERE User.user_id = tips.user_id
/*UPDATE Statement for tipCount Ends*/