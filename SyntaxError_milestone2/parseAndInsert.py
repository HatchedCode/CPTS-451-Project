import json
import psycopg2

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2BusinessTable():
    #reading the JSON file
    with open('./yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='none'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO businessTable (business_id, name, address, state, city, zipcode, latitude, longitude, stars, numCheckins, numTips, openStatus) " \
                      "VALUES ('" + data['business_id'] + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "','" + data["postal_code"] + "'," + str(data["latitude"]) + "," + \
                      str(data["longitude"]) + "," + str(data["stars"]) + ", 0 , 0 ,"  +  str(data["is_open"]) + ");"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to businessTABLE failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2UserTable():
    #reading the JSON file
    with open('./yelp_user.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
            line = f.readline()
            count_line = 0

            #connect to yelpdb database on postgres server using psycopg2
            #TODO: update the database name, username, and password
            try:
                conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='none'")
            except:
                print('Unable to connect to the database!')
            cur = conn.cursor()

            while line:
                data = json.loads(line)
                # Generate the INSERT statement for the cussent business
                # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
                # include values for all businessTable attributes
                sql_str = "INSERT INTO userTable (user_id, name, yelping_since, tipcount, fans, average_stars, (funny,useful,cool)) " \
                          "VALUES ('" + data['user_id'] + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["yelping_since"]) + "','" + str(data["tipcount"]) + "','" + str(data["fans"]) + "','" + str(data["average_stars"]) + "','" + "(" + str(data["funny"]) + "," + str(data["useful"]) + "," + str(data["cool"]) + ")" + ");"
                try:
                    cur.execute(sql_str)
                except:
                    print("Insert to userTABLE failed!")
                conn.commit()
                # optionally you might write the INSERT statement to a file.
                # outfile.write(sql_str)

                line = f.readline()
                count_line +=1

            cur.close()
            conn.close()

        print(count_line)
        #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
        f.close()

def insert2CheckinTable():
    #reading the JSON file
    with open('./yelp_checkin.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='none'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            
            dates = data["date"].split(',') #we split the line by comma to get each day and the corresponding time.
            for cur_day in dates: #Loop through all of the days
                (day,time) = cur_day.split(' ') #Split the day and the time
                (year,month,day) = day.split('-') #Split the day into the year, month, and day.
            
            sql_str = "INSERT INTO checkinTable (business_id, (year, month, day), time)" \
                      "VALUES ('" + data['business_id'] + "','" + str(year, month, day) + "','" + str(time) + ");"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to checkinTABLE failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2TipTable():
    #reading the JSON file
    with open('./yelp_tip.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='none'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            
            dates = data["date"].split(',') #we split the line by comma to get each day and the corresponding time.
            for cur_day in dates: #Loop through all of the days
                (day,time) = cur_day.split(' ') #Split the day and the time
                (year,month,day) = day.split('-') #Split the day into the year, month, and day.
            
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO checkinTable (business_id, (year, month, day), likes, text, user_id) " \
                                 "VALUES ('" + data['business_id'] + "','" + str((year, month, day)) + "','" + str(data["likes"]) "','" + cleanStr4SQL(str(data["text"])) "','" + cleanStr4SQL(data["user_id"]) + ");"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to tipTABLE failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()
    
    
insert2BusinessTable()
insert2UserTable()
insert2CheckinTable()
insert2TipTable()
