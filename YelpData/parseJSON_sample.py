import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def parseBusinessData():
    #read the JSON file
    with open('yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
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
            outfile.write(str(data['is_open'])+'\t') #openstatus

            categories = data["categories"].split(', ')
            outfile.write(str(categories)+'\t')  #category list
            
            outfile.write(str([])) # write your own code to process attributes
            outfile.write(str([])) # write your own code to process hours
            outfile.write('\n');

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

def parseUserData():
    #write code to parse yelp_user.JSON
    with open('yelp_user.JSON', 'r') as f:
        outfile = open('user.txt', 'w')
        line = f.readline()
        count_line = 0
        # read each JSON object and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['average_stars'])+'\t') #average_stars
            outfile.write(cleanStr4SQL(data['cool'])+'\t') #cool
            outfile.write(cleanStr4SQL(data['fans'])+'\t') #fans
            
            # Friends is an array and needs to parse the strings inside of it
            outfile.write(cleanStr4SQL(data['friends'])+'\t') #friends
            
            outfile.write(cleanStr4SQL(data['funny'])+'\t') #funny
            outfile.write(cleanStr4SQL(data['name'])+'\t') #name
            outfile.write(cleanStr4SQL(data['tipcount'])+'\t') #tipcount
            outfile.write(cleanStr4SQL(data['useful'])+'\t') #useful
            outfile.write(cleanStr4SQL(data['user_id'])+'\t') #user_id
            outfile.write(cleanStr4SQL(data['yelping_since'])+'\t') #yelping_since
            
            line = f.readline()
            count_line += 1
    print(count_line)
    outfile.close()
    f.close()
    pass

def parseCheckinData():
    #write code to parse yelp_checkin.JSON
    with open('yelp_checkin.JSON', 'r') as f:
        outfile = open('checkin.txt', 'w')
        line = f.readline()
        count_line = 0
        # read each JSON object and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business_id
            
            #Need to split this string and extract check-in timestamps for the business
            outfile.write(cleanStr4SQL(data['date'])+'\t') #date
            
            line = f.readline()
            count_line += 1
    print(count_line)
    outfile.close()
    f.close()
    pass
    

# Based on the JSON file, this one looks to be complete already
def parseTipData():
    #write code to parse yelp_tip.JSON
    with open('yelp_tip.JSON', 'r') as f:
        outfile = open('tip.txt', 'w')
        line = f.readline()
        count_line = 0
        # read each JSON object and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business_id
            outfile.write(cleanStr4SQL(data['date'])+'\t') #date
            outfile.write(cleanStr4SQL(data['likes'])+'\t') #likes
            outfile.write(cleanStr4SQL(data['text'])+'\t') #text
            outfile.write(cleanStr4SQL(data['user_id'])+'\t') #user_id
            
            line = f.readline()
            count_line += 1
    print(count_line)
    outfile.close()
    f.close()
    pass

#REMOVE PASS TO RUN THE OTHER FUNCTIONS
parseBusinessData()
parseUserData()
parseCheckinData()
parseTipData()
