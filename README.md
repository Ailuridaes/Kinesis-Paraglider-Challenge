# Kinesis Paraglider Challenge Team Hackathon Challenge

Use AWS's Kinesis service to find the optimal time to go Paragliding!  We will be using weather data provided by NOAA with a provided event streamer. 

<image src='https://scijinks.gov/review/noaa/noaa-logo.png' width='300px' alt='NOAA' /><image src='https://images.unsplash.com/photo-1440130266107-787dd24d69d7?ixlib=rb-0.3.5&ixid=eyJhcHBfaWQiOjEyMDd9&s=9b8f0de6077b535709700b6f79ed6db8&auto=format&fit=crop&w=1647&q=80' width='300px' alt='Paragliding' />

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
- Goal - Use Kinesis Data Analytics to add a lambda function for pre-processing records
  - Replace `NA` with values of zero on field `barometricPressure`.
  - Check output of the processing of the new lambda function
- Helpful docs
  - https://docs.aws.amazon.com/kinesisanalytics/latest/dev/getting-started.html
<details><summary>Hints</summary>
- Be very careful with the IAM role for DA permissions
- Make sure the data streaming application is running when using DA
- Data is base64 encoded!
</details>

# BOSS Level - Kinesis Data Anlalytics using SQL Editor
- Within the Kinesis Data Anlytics Application you created for step 4 use the SQL editor to 