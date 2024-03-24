# pokerbunch

Frontend for pokerbunch.com

## Local development

- Clone backend project `https://github.com/nolll/pokerbunch-api.git`
- `docker compose up` to setup the database
- Open backend solution in vs and hit F5
- Clone frontend project `https://github.com/nolll/pokerbunch.git`
- `npm install`
- `npm run watch`
- Head to `https://localhost:9000`
- Login with a user: `admin`, `manager` or `player`. The password is `abcd`

Some actions sends emails. For that to work, install smtp4dev
`dotnet tool install -g Rnwood.Smtp4dev` and run it with `smtp4dev`
