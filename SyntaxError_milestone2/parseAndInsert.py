import json
import psycopg2

mylist = []

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

#attributes are inside a dictionary and the dictionary can be nested
def attParser(mydict):
    for key, value in mydict.items():
        if type(value) == dict:
            attParser(value)
        else:
            mylist.append(tuple((str(key), str(value))))
    return mylist

#hours are inside a dictionary and the hours are seperated by '-'
def hourParser(mydict, file):
    for key, item in mydict.items(): #Access the first dictionary; get key and item
        str(item) #typecast the item into a string
        mylist.append(tuple((str(key), item.split('-')))) #make into a tuple and append to list    


def insert2BusinessTable():
    #reading the JSON file
    with open('./YelpData/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./INSERTdata/yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            businessID = data['business_id']
            sql_str_businessTable = "INSERT INTO BusinessTable (businessID, busName, busAddress, busState, busCity, busPostal, latitude, longitude, stars, rev_count, is_Open, numCheckins, numTips) " \
                      "VALUES ('" + businessID + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "','" + data["postal_code"] + "'," + str(data["latitude"]) + "," + \
                      str(data["longitude"]) + "," + str(data["stars"]) + "," + str(data["review_count"]) + "," + str(data["is_open"]) + ", 0, 0" + ");"
            try:
                cur.execute(sql_str_businessTable)
            except:
                print("Insert to BusinessTable failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            outfile.write(sql_str_businessTable)
            outfile.write("\n")
            outfile.write("\n")

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2CategoryTable():
    #reading the JSON file
    with open('./YelpData/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./INSERTdata/yelp_category.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all Category attributes
            businessID = str(data['business_id'])
            categories = data["categories"].split(', ')
            Categories = [item for item in categories]

            for cat_name in Categories:
                sql_str_categoryTable = "INSERT INTO CategoryTable (cat_name, businessID) " \
                        "VALUES ('" + cleanStr4SQL(cat_name) + "','" + businessID +  "'" + ");"
                try:
                    cur.execute(sql_str_categoryTable)
                except Exception as e:
                    print(e)
                    print("Insert to CategoryTable failed!")
                conn.commit()
                # optionally you might write the INSERT statement to a file.
                outfile.write(sql_str_categoryTable)
                outfile.write("\n")
                outfile.write("\n")

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2AttritutesTable():
    #reading the JSON file
    with open('./YelpData/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./INSERTdata/yelp_attritutes.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all Category attributes
            businessID = str(data['business_id'])
            mylist = []
            attParser(data['attributes'])

            for (att_name, value) in mylist:
                sql_str_attritutesTable = "INSERT INTO AttributesTable (att_name, value, businessID) " \
                        "VALUES ('" + att_name + "','" + value  + "','" + businessID +  "'" + ");"
                try:
                    cur.execute(sql_str_attritutesTable)
                except:
                    print("Insert to AttributesTable failed!")
                conn.commit()
                # optionally you might write the INSERT statement to a file.
                outfile.write(sql_str_attritutesTable)
                outfile.write("\n")
                outfile.write("\n")
                

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()


def insert2HoursTable():
    #reading the JSON file
    with open('./YelpData/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./INSERTdata/yelp_hours.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all Category attributes
            businessID = data['business_id']
            mylist = []
            hourParser(data['hours'], outfile)

            for (h_day, value) in mylist:
                t_start = value[0] #Given the structure that there is only an open and an end in the list (Nothing more).
                t_end = value[1]
                sql_str_attritutesTable = "INSERT INTO HoursTable (t_start, t_end, h_day, businessID) " \
                        "VALUES ('" + t_start + "','" + t_end + "','" + h_day  + "','" + businessID +  "'" + ");"
                try:
                    cur.execute(sql_str_attritutesTable)
                except:
                    print("Insert to HoursTable failed!")
                conn.commit()
                # optionally you might write the INSERT statement to a file.
                outfile.write(sql_str_attritutesTable)
                outfile.write("\n")
                outfile.write("\n")

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()


def insert2FriendTable():
    #reading the JSON file
    with open('./YelpData/yelp_user.JSON','r') as f:    #TODO: update path for the input file
            outfile =  open('./INSERTdata/yelp_friend.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
            line = f.readline()
            count_line = 0

            #connect to yelpdb database on postgres server using psycopg2
            #TODO: update the database name, username, and password
            try:
                conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
            except:
                print('Unable to connect to the database!')
            cur = conn.cursor()

            while line:
                data = json.loads(line)
                # Generate the INSERT statement for the cussent business
                # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
                # include values for all businessTable attributes
                user_id = cleanStr4SQL(data['user_id'])
                friends = (data['friends'])
                friendsList = ([item for item in friends])  # friend list

                for friend in friendsList:
                    sql_str_friendTable = "INSERT INTO FriendTable (current_user_id, friend_user_id) " \
                            "VALUES ('" + user_id + "','" + cleanStr4SQL(friend) +  "'" + ");"
                    try:
                        cur.execute(sql_str_friendTable)
                    except:
                        print("Insert to FriendTable failed!")
                    conn.commit()
                    # optionally you might write the INSERT statement to a file.
                    outfile.write(sql_str_friendTable)
                    outfile.write("\n")
                    outfile.write("\n")

                line = f.readline()
                count_line +=1

            cur.close()
            conn.close()

    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2UserTable():
    #reading the JSON file
    with open('./YelpData/yelp_user.JSON','r') as f:    #TODO: update path for the input file
            outfile =  open('./INSERTdata/yelp_user.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
            line = f.readline()
            count_line = 0

            #connect to yelpdb database on postgres server using psycopg2
            #TODO: update the database name, username, and password
            try:
                conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
            except:
                print('Unable to connect to the database!')
            cur = conn.cursor()

            while line:
                data = json.loads(line)
                # Generate the INSERT statement for the cussent business
                # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
                # include values for all businessTable attributes
                sql_str_userTable = "INSERT INTO UserTable (user_id, name, funny, yelping_since, useful, fans, cool, average_stars, tipcount, postcount, likecount, longitude, latitude) " \
                          "VALUES ('" + data['user_id'] + "','" + cleanStr4SQL(data["name"]) + "','" + str(data["funny"]) + "','" + cleanStr4SQL(data["yelping_since"]) + "','" + \
                            str(data["useful"]) + "','" + str(data["fans"]) + "','" + str(data["cool"]) + "','" + str(data["average_stars"]) + "','" + str(data["tipcount"]) + \
                            "','" + str(0)  + "','" + str(0) + "','" + str(float(0.0)) + "','" + str(float(0.0)) + "' " + ");"
                try:
                    cur.execute(sql_str_userTable)
                except:
                    print("Insert to UserTable failed!")
                conn.commit()
                # optionally you might write the INSERT statement to a file.
                outfile.write(sql_str_userTable)
                outfile.write("\n")
                outfile.write("\n")

                line = f.readline()
                count_line +=1

            cur.close()
            conn.close()

    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2CheckinTable():
    #reading the JSON file
    with open('./YelpData/yelp_checkin.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./INSERTdata/yelp_checkin.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            
            businessID = cleanStr4SQL(data['business_id'])
            dates = data["date"].split(',') #we split the line by comma to get each day and the corresponding time.
            for cur_day in dates: #Loop through all of the days
                (day,time) = cur_day.split(' ') #Split the day and the time
                (year,month,day) = day.split('-') #Split the day into the year, month, and day.
            
                sql_str_checkinTable = "INSERT INTO CheckInTable (day, time, year, month, businessID) " \
                            "VALUES ('" + str(day) + "','" + str(time) + "','" + str(year) + "','" + str(month) + "','" + str(businessID) + "'" + ");"
                try:
                    cur.execute(sql_str_checkinTable)
                except Exception as e:
                    print(e)
                    print("Insert to CheckInTable failed!")
                conn.commit()
                # optionally you might write the INSERT statement to a file.
                outfile.write(sql_str_checkinTable)
                outfile.write("\n")
                outfile.write("\n")

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2TipTable():
    #reading the JSON file
    with open('./YelpData/yelp_tip.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./INSERTdata/yelp_tip.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='postgres'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            
            (date,time) = cleanStr4SQL(data['date']).split(' ') #Split the day and the time
            # (year,month,day) = day.split('-') #Split the day into the year, month, and day.

            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str_tipTable = "INSERT INTO TipTable (businessID, user_id, likes, text, date) " \
                                 "VALUES ('" + data['business_id'] + "','" + cleanStr4SQL(data["user_id"])  + "','" + \
                                    str(data["likes"]) + "','" + cleanStr4SQL(data["text"])+ "','" + str(date) +  "'" + ");"
            try:
                cur.execute(sql_str_tipTable)
            except Exception as e:
                print(e)
                print("Insert to TipTable failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            outfile.write(sql_str_tipTable)
            outfile.write("\n")
            outfile.write("\n")

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()
    

insert2BusinessTable()
insert2UserTable()
insert2CheckinTable()
insert2TipTable()
insert2FriendTable()
insert2HoursTable()
insert2AttritutesTable()
insert2CategoryTable()