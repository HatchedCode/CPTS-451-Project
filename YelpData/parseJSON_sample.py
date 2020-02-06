import json

mylist = []

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

#attributes are inside a dictionary and the dictionary can be nested
#def attParser(mydict, file):
#    mylist = []
#    for key, item in mydict.items(): #Access the first dictionary; get key and item
#        if type(item) == dict: #checking the type of item
#            for key2, item2 in item.items(): #if item is dictionary, repeat
#                mylist.append(tuple((str(key2), str(item2)))) #make into tuple and append to list  
#        else:
#            mylist.append(tuple((str(key), str(item)))) #make into tuple and append to list
#    file.write(str(mylist) + "\n") #write this list to the outfile
    
def attParser(mydict):
    for key, value in mydict.items():
        if type(value) == dict:
            attParser(value)
        else:
            mylist.append(tuple((str(key), str(value))))
    return mylist
            
    
#hours are inside a dictionary and the hours are seperated by '-'
def hourParser(mydict, file):
    mylist = []
    for key, item in mydict.items(): #Access the first dictionary; get key and item
        str(item) #typecast the item into a string
        mylist.append(tuple((str(key), item.split('-')))) #make into a tuple and append to list     
    file.write(str(mylist) + "\n") #write this list to the outfile

def parseBusinessData():
    #read the JSON file
    #{business_id: str, name: str, address: str, city: str, state: str, postal: str,
    # lat: float, long: float, stars: float, rev_count: int, open: int, attributes: dict,
    # categories: str, hours: dict}
    global mylist 
    with open('yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        outfile.write("HEADER: (business_id, name; address; state; state; city; postal_code; latitude; longitude; stars; is_open)\n")
        while line:
            data = json.loads(line)
            outfile.write(str(count_line + 1) + "- business info: ")
            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business id
            outfile.write(cleanStr4SQL(data['name'])+'\t') #name
            outfile.write(cleanStr4SQL(data['address'])+'\t') #full_address
            outfile.write(cleanStr4SQL(data['state'])+'\t') #state
            outfile.write(cleanStr4SQL(data['city'])+'\t') #city
            outfile.write(cleanStr4SQL(data['postal_code']) + '\t')  #zipcode
            outfile.write(str(data['latitude'])+'\t') #latitude
            outfile.write(str(data['longitude'])+'\t') #longitude
            outfile.write(str(data['stars'])+'\t') #stars
            outfile.write(str(data['review_count'])+'\t') #reviewcount
            outfile.write(str(data['is_open'])+'\n') #openstatus
            
            #parsing categories
            categories = data["categories"].split(', ')
            outfile.write("\tcategories: ")
            outfile.write(str([item for item in categories])+'\n')  #category list
            
            #parsing attributes
            outfile.write("\tattributes: ")
            outfile.write(str(attParser(data['attributes'])) + '\n')
            mylist = []
            #attParser(data['attributes'], outfile)
            
            #parsing hours
            outfile.write("\thours: ")
            hourParser(data['hours'], outfile)
            outfile.write('\n');

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

def parseUserData():
    #write code to parse yelp_user.JSON
#    with open('yelp_user.JSON', 'r') as f:
#        outfile = open('user.txt', 'w')
#        line = f.readline()
#        count_line = 0
#        # read each JSON object and extract data
#        while line:
#            data = json.loads(line)
#            outfile.write(cleanStr4SQL(data['average_stars'])+'\t') #average_stars
#            outfile.write(cleanStr4SQL(data['cool'])+'\t') #cool
#            outfile.write(cleanStr4SQL(data['fans'])+'\t') #fans
#            
#            # Friends is an array and needs to parse the strings inside of it
#            outfile.write(cleanStr4SQL(data['friends'])+'\t') #friends
#            
#            outfile.write(cleanStr4SQL(data['funny'])+'\t') #funny
#            outfile.write(cleanStr4SQL(data['name'])+'\t') #name
#            outfile.write(cleanStr4SQL(data['tipcount'])+'\t') #tipcount
#            outfile.write(cleanStr4SQL(data['useful'])+'\t') #useful
#            outfile.write(cleanStr4SQL(data['user_id'])+'\t') #user_id
#            outfile.write(cleanStr4SQL(data['yelping_since'])+'\t') #yelping_since
#            
#            line = f.readline()
#            count_line += 1
#    print(count_line)
#    outfile.close()
#    f.close()
    pass

def parseCheckinData():
    #write code to parse yelp_checkin.JSON
#    with open('yelp_checkin.JSON', 'r') as f:
#        outfile = open('checkin.txt', 'w')
#        line = f.readline()
#        count_line = 0
#        # read each JSON object and extract data
#        while line:
#            data = json.loads(line)
#            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business_id
#            
#            #Need to split this string and extract check-in timestamps for the business
#            outfile.write(cleanStr4SQL(data['date'])+'\t') #date
#            
#            line = f.readline()
#            count_line += 1
#    print(count_line)
#    outfile.close()
#    f.close()
    pass
    

# Based on the JSON file, this one looks to be complete already
def parseTipData():
    #write code to parse yelp_tip.JSON
#    with open('yelp_tip.JSON', 'r') as f:
#        outfile = open('tip.txt', 'w')
#        line = f.readline()
#        count_line = 0
#        # read each JSON object and extract data
#        while line:
#            data = json.loads(line)
#            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business_id
#            outfile.write(cleanStr4SQL(data['date'])+'\t') #date
#            outfile.write(cleanStr4SQL(data['likes'])+'\t') #likes
#            outfile.write(cleanStr4SQL(data['text'])+'\t') #text
#            outfile.write(cleanStr4SQL(data['user_id'])+'\t') #user_id
#            
#            line = f.readline()
#            count_line += 1
#    print(count_line)
#    outfile.close()
#    f.close()
    pass

#REMOVE PASS TO RUN THE OTHER FUNCTIONS
parseBusinessData()
parseUserData()
parseCheckinData()
parseTipData()
