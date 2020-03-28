"SELECT businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busPostal";

SELECT CategoryTable.cat_name
FROM 
(
    "SELECT businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busPostal";
) as business, CategoryTable
NATURAL JOIN ON CategoryTable.businessID = business.businessID
GROUP BY CategoryTable.cat_name





SELECT busName, businessID
FROM BusinessTable WHERE busPostal = '85086'
ORDER BY busName, businessID;




SELECT distinct bus.busName, bus.businessID
FROM 
    (
        SELECT busName, businessID
        FROM BusinessTable
        WHERE busPostal = '85086'
        ORDER BY busPostal
    ) as bus
    FULL OUTER JOIN CategoryTable ON CategoryTable.businessID = bus.businessID AND CategoryTable.cat_name = 'Acne Treatment'
ORDER BY bus.busName, bus.businessID