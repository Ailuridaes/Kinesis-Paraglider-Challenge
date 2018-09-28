# Kinesis Paraglider Challenge Team Hackathon Challenge

Use AWS's Kinesis service to find the optimal time to go Paragliding! 

## Pre-requisites
The following tools and accounts are required to complete these instructions.

* [Sign-up for an AWS account](https://aws.amazon.com/)
* [Install AWS CLI](https://aws.amazon.com/cli/)
* [NodeJs](https://nodejs.org/en/)
* [Install .NET Core 2.1](https://www.microsoft.com/net/download)

# Level 0

- Create a Kinesis stream in AWS console.
- Update kinesis key name in `src/WeatherStationsEvents/appsettings.json`.
- Build and run `src/WeatherStationsEvents`

# Level 1

 - Here is a ready to go Lambda Function for reading a Kinesis Stream. Make it output the data to Cloudwatch Logs

```javascript
exports.handler = (event, context, callback) => {
    
    for (let i = 0; i < event.Records.length; i++) {
        const eventRecord = JSON.parse(Buffer.from(event.Records[i].kinesis.data, 'base64'));
       //TODO
    }
    
    callback(null, "Hello from Lambda");
};
```

# Level 2 - Single Site Notification
- Goal - Find the best time to go fly at Torrey Pines Gliderport
- Conditions
  - <80% humidity
  - Wind 230 to 290 degrees at 6-12 knots. Gusts below 20
- Trigger a notification to inform when conditions are good to fly
- Send info to Cloudwatch logs

# Level 3 - Multi Site Notification
- Goal - Find the best time to go fly at more than one location
- Use this website to determine the best conditions at another location or locations
  - https://www.sdhgpa.com/sites-guide.html
- Integrate AWS SNS to SMS service to send a notification to yourself
- Only send one notification per site per day
- Only notify during daylight hours - 9 AM to 6 PM
- Helpful docs
  - https://docs.aws.amazon.com/sns/latest/dg/SubscribeTopic.html
  - https://docs.aws.amazon.com/sdk-for-javascript/v2/developer-guide/sns-examples-publishing-messages.html

# Level 4 - Kinesis Data Analytics
//Research - explore data analytics use cases
- Use Kinesis Data Analytics to add a lambda function for pre-processing of records. Replace `NA` with values of zero on field `barometricPressure`.
  - https://docs.aws.amazon.com/kinesisanalytics/latest/dev/getting-started.html
