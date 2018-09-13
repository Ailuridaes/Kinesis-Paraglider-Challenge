# Kinesis-Paraglider-Challenge

Goal - Find the optimal time to go Paragliding

# Level 0

- Create a Kinesis stream in AWS console.
- Update kinesis key name in `src/WeatherStationsEvents/appsettings.json`.
- Build and run `src/WeatherStationsEvents`

# Level 1

 - Here is a ready to go Lambda Function for reading a Kinesis Stream. Make it output the data to Cloudwatch Logs

```javascript
exports.handler = (event, context, callback) => {
    
    for (let i = 0; i < event.Records.length; i++) {
        const eventRecord = Buffer.from(event.Records[i].kinesis.data, 'base64');
       //TODO
    }
    
    callback(null, "Hello from Lambda");
};
```

# Level 2 - Single Site Notification
- Goal - Find the best time to go fly at Torrey Pines Gliderport
- 70 - 80 deg
- <50% humidity
- Wind SW to NW at 10-12 knots. Gusts below 15
- Trigger a notification to inform when conditions are good to fly
- Send info to Cloudwatch logs

# Level 3
- Multi Site Notification
- One notification per site per day
- Only notifiy during daylight hours - 9 AM to 6 PM
- Integrate AWS SNS to SMS service to send a notification to yourself
//TODO add link to SNS documentation

# Level 4 - Kinesis Data Analytics
//Research - explore data analytics use cases