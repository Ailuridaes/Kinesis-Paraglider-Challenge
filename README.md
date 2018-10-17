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

- Create a Kinesis stream in the AWS console
  - Search for `Kinesis` in the services dropdown
  - Click `Create data steam` under the Kinesis data streams section on the Amazon Kinesis Dashboard
  - Give it a name and 1 shard
- Clone this repo to get the data streamer application on your machine
- Update kinesis key name in `src/WeatherStationsEvents/appsettings.json` to match the name you just gave to your stream
<details><summary>Build and run 'src/WeatherStationsEvents'</summary>
<ul>
  <li>Go to terminal</li>
  <li>CD into the repo you just cloned</li>
  <li>CD into 'src/WeatherStationEvents'</li>
  <li>Run 'dotnet restore' and 'dotnet run'</li>
  <li>Verify from logs in the terminal that events are being generated</li>
  <li>NOTE: This data is historical. It starts from an arbitrary date in August 2018 and continues up to today.</li>
</ul>
</details>


# Level 1
Goal - Create a lambda function to capture the streaming data from the Kinesis stream you just set up.

- Here is a ready to go NodeJs 6.10 lambda function for reading a Kinesis Stream. Make the lambda function output the data to CloudWatch Logs

```javascript
exports.handler = (event, context, callback) => {
    
    for (let i = 0; i < event.Records.length; i++) {
      const eventRecord = JSON.parse(Buffer.from(event.Records[i].kinesis.data, 'base64'));
      console.log(eventRecord); 
    }
    
    callback(null, "Hello from Lambda");
};
```
<details><summary>Make sure to set up this function to trigger off of the Kinesis Stream</summary>
<ul>
  <li>Navigate to the AWS console for your lambda function</li>
  <li>Make sure the configuration tab is selected at the top of the page</li>
  <li>From the list of triggers on the left panel in the Designer, choose Kinesis</li>
  <li>Scroll down to the 'Configure triggers' section</li>
  <li>Select the Kinesis Stream you previously created from the dropdown</li>
  <li>Make sure the 'Enable trigger' box is checked, then hit 'Add'</li>
</ul>
</details>
<details><summary>Check CloudWatch logs for event record output</summary>
<ul>
  <li>Once the trigger is setup, run the streaming application from the terminal</li>
  <li>A record should be pushed to the Kinesis stream every five seconds and processed by your lambda function</li>
  <li>On the lambda function page, click the 'Monitoring' tab at the top and click the 'View logs in CloudWatch button on the right</li>
</ul>
</details>

# Level 2 - Single Site Notification
Goal - Find the best time to go fly at **Torrey Pines Gliderport**. Analyze the streaming data and determine if the weather is good for paragliding.

- Conditions - If the conditions at Torrey Pines satisfy these, then it's good to fly!
  - Less than 80% humidity
  - Wind direction 230 to 290 degrees
  - Wind speed 6 to 12 knots
  - Gusts below 20 knots
- Trigger a message to CloudWatch logs to inform when conditions are good to fly
- Integrate AWS SNS to SMS service to send a notification to yourself
- Helpful docs
  - https://docs.aws.amazon.com/sns/latest/dg/SubscribeTopic.html
  - https://docs.aws.amazon.com/sdk-for-javascript/v2/developer-guide/sns-examples-publishing-messages.html
<details><summary>Hints</summary>
<ul>
  <li>Explore the event record object to find the attributes that need to be checked</li>
</ul>
</details>

# Level 3 - Kinesis Data Analytics
Goal - Use Kinesis Data Analytics to add a lambda function for pre-processing records.

  - Replace `NA` with values of zero on field `barometricPressure`
- Hook up the output of the pre processing to your original lambda function
<details><summary>Helpful docs</summary>
<ul>
  <li>https://docs.aws.amazon.com/kinesisanalytics/latest/dev/getting-started.html</li>
</ul>
</details>
<details><summary>Hints</summary>
<ul>
  <li>Be very careful with the IAM role for Data Analytics permissions</li>
  <li>Make sure the data streaming application is running when using DA</li>
  <li>Data is base64 encoded!</li>
</ul>
</details>

# Level 4 - Multi Site Notification
Goal - Find the best time to go fly at more than one location.

- Use this website to determine the best conditions at another location or locations
  - https://www.sdhgpa.com/sites-guide.html
  - Choose one (or more) of the 5 sites under 'Primary Sites'
  - Look at the description of the site and find the recommended flying conditions at that site
- Update your lambda function to check the streaming data for flying conditions at multiple sites
- Update the function to: 
  - Only send one notification per site per day
  - Only notify during daylight hours - 9 AM to 6 PM
<details><summary>Hints</summary>
<ul>
  <li>You will have to persist the data across lambda invocations in order to know if a notification has already been sent...</li>
</ul>
</details>

# BOSS Level - Real Time Analytics with SQL
<p><a target="_blank" rel="noopener noreferrer" href="https://camo.githubusercontent.com/24ee58920381e83562f9780036a8df86ef9dec18/687474703a2f2f696d61676573322e66616e706f702e636f6d2f696d6167652f70686f746f732f31303430303030302f426f777365722d6e696e74656e646f2d76696c6c61696e732d31303430333230332d3530302d3431332e6a7067"><img src="https://camo.githubusercontent.com/24ee58920381e83562f9780036a8df86ef9dec18/687474703a2f2f696d61676573322e66616e706f702e636f6d2f696d6167652f70686f746f732f31303430303030302f426f777365722d6e696e74656e646f2d76696c6c61696e732d31303430333230332d3530302d3431332e6a7067" alt="boss" data-canonical-src="http://images2.fanpop.com/image/photos/10400000/Bowser-nintendo-villains-10403203-500-413.jpg" style="max-width:100%;"></a></p>
Goal - Use Kinesis Data Analytics to create a new data stream to better fit for the original lambda function application.

- Within the Kinesis Data Analytics Application you created for step 3 use the SQL editor to perform real time analytics on the data
- Create a new SQL query using the templated SQL examples. Use the source data as a guide
- Attach the resulting stream of the real time analytics to your original lambda function