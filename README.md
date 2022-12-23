
# Laboratorinio darbo ataskaita

### Sistemos paskirtis:

##### Projekto tikslas: 

Sudaryti tinklo aplikaciją, kurioje būtų galima matyti rodomus filmus skirtinguose kino 
teatruose, o po to esant norui nusipirki bilietą į norimą seansą.

##### Tinklo aplikacijos veikimo principas: 

Darbuotojas, kurio paskyrą sukuria administratorius, naudodamasis platforma turės 
galimybę pridėti, pašalinti arba redaguoti norimus filmus, seansus arba kino teatrus. 
Darbuotojas, norėdamas naudotis šia platforma, prisiregistruos prie internetinės aplikacijos. 
Prieš pridėdant naują filmą arba kino teatrą, administratorius turi tai patvirtinti.

---------------------------------------------------------------------------------------------------------------------

### Funkciniai reikalavimai

 #### Neregistruotas sistemos naudotojas galės: 
 
1. Peržiūrėti rodomus filmus. 
2. Prisijungti prie internetinės aplikacijos.

#### Registruotas sistemos naudotojas galės: 

1. Atsijungti nuo internetinės aplikacijos. 
2. Prisijungti prie platformos. 
3. Pridėti/Šalinti/Redaguoti filmą.
4. Pridėti/Šalinti/Redaguoti kino teatrą.
5. Pridėti/Šalinti/Redaguoti seansą.

#### Administratorius galės: 

1. Sudaryti naujo darbuotojo paskyrą. 
2. Šalinti naudotojus. 
3. Patvirtinti naujus filmus bei kino teatrus.

---------------------------------------------------------------------------------------------------------------------

## Sistemos architektūra

#### Sistemos sudedamosios dalys: 

• Kliento pusė - naudojant „React.js“ karkasą. 

• Serverio pusė - naudojant „ASP.NET“ karkasą. Duomenų bazė - „MySQL“. 

![Sistemos diegimo diagrama](https://github.com/lure110/Saitynu_projektas_Valda/blob/main/uml_deployment.png)

---------------------------------------------------------------------------------------------------------------------

## Naudotojo sąsajos projektas:

| Wireframe | Realizacija|
|-----------|------------|
|![Header wireframe](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Home.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Home.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/header.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/header.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Login.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Login.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Register.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Register.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/CreateCinema.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/CreateCinema.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/EditCinema.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/EditCinema.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/EditMovie.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/EditMovie.png)|
|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Screening.png)|![enter image description here](https://github.com/NaktinisApuokas/FobumCinema/blob/main/Screening.png)|


---------------------------------------------------------------------------------------------------------------------

## API specifkacija

### Get, GetById

#### GET api/cinemas

Gražina visus kino teatrus.

#### Galimi atsako kodai : 200

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    [
	    {
		"id": 29,
		"name": "update",
		"address": "address"
	    }
    ]
  **Kai duomenų bazė neturi duomenų:**

    []

#### GET api/cinemas/id

Gražina nurodytą kino teatrą.

#### Galimi atsako kodai: 200, 404

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**29**

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    {
		"id": 29,
		"name": "update",
		"address": "address"
	}
  **Kai duomenų bazė neturi duomenų:**

Cinema with id '23' not found.

#### GET api/cinemas/cinemasId/movies

Gražina visus filmus nurodytame kino teatre.

#### Galimi atsako kodai: 200

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**29**/movies

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    [
		{
			"id": 3,
			"title": "test",
			"genre": "ttest",
			"description": "test"
		},
		{
			"id": 6,
			"title": "title",
			"genre": "fatastic",
			"description": "awesome movie"
		}
	]

**Kai duomenų bazė neturi duomenų:**

    []
    
    

#### GET api/cinemas/cinemasId/movies/id

Gražina nurodyta filmą, kuris yra rodomas nurodytame kino teatre.

#### Galimi atsako kodai: 200, 404

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    {
		"id": 3,
		"title": "test",
		"genre": "ttest",
		"description": "test"
	}

  **Kai duomenų bazė neturi duomenų:**

    {
		"type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
		"title": "Not Found",
		"status": 404,
		"traceId": "00-c261ff51b08be0de5d1d832210c209bb-67c91b105bb5a507-00"
	}



#### GET api/cinemas/cinemasId/movies/moviesId/screenings

Gražina visus seansus, nurodytame filme, kuris yra rodomas nurodytame kino teatre.

#### Galimi atsako kodai: 200

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**/screening

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

	[
		{
			"id": 5,
			"hall": 4,
			"seatnumber": 50,
			"price": 5.5
		}
	]

**Kai duomenų bazė neturi duomenų:**

    []
    
    

#### GET api/cinemas/cinemasId/movies/lmoviesId/screenings/id

Gražina nurodytą seansą, nurodytame filme, kuris yra rodomas nurodytame kino teatre.

#### Galimi atsako kodai: 200, 404

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**/screenings/**1**

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    {
		"id": 5,
		"hall": 4,
		"seatnumber": 50,
		"price": 5.5
	}

  **Kai duomenų bazė neturi duomenų:**

    {
		"type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
		"title": "Not Found",
		"status": 404,
		"traceId": "00-9862c41b7cabe182e79d824b1e71ee27-67d7bd6fa9a1c9be-00"
	}


---------------------------------------------------------------------------------------------------------------------

#### Post

Visų Post galimi atsako kodai vienodi: 201


#### POST api/cinemas

Sukuria naują kino teatrą pagal pateiktus duomenis.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas

##### JSON duomenys:

    {
		"name":"post",
		"address":"address"
	}

##### Atsakymas:

    {
		"id": 50,
		"name": "post",
		"address": "address"
	}


#### POST api/cinemas/cinemasId/movies

Sukuria naują filmą pagal pateiktus duomenis.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies

##### JSON duomenys:

    {
		"Title":"shrek",
		"Genre":"fantasy",
		"Description":"very good"
	}

##### Atsakymas:

    {
		"id": 7,
		"title": "shrek",
		"genre": "fantasy",
		"description": "very good"
	}


#### POST api/cinemas/cinemasId/movies/moviesId/sreenings

Sukuria naują seansą pagal pateiktus duomenis.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**/sreenings

##### JSON duomenys:

    {
		"hall":"10",
		"seatnumber":"199",
		"price":"10"
	}

##### Atsakymas:

	{
		"hall":"10",
		"seatnumber":"199",
		"price":"10"
	}


---------------------------------------------------------------------------------------------------------------------

### Delete

Visų Delete galimi atsako kodai vienodi: 204

#### DELETE api/cinemas/id

Panaikiną nurodytą kino teatrą.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**

##### Atsakymas:

Gražinamas 204 kodas


#### DELETE api/cinemas/cinemasId/movies/id

Panaikiną nurodytą filmą.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**

##### Atsakymas:

Gražinamas 204 kodas


#### DELETE api/cinemas/cinemasId/movies/id/screening/id

Panaikiną nurodytą seansą.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**/screening/**1**

##### Atsakymas:

Gražinamas 204 kodas


---------------------------------------------------------------------------------------------------------------------

#### Put

Visų Put galimi atsako kodai vienodi: 200

#### PUT api/cinemas/id

Koreguoja nurodyta kino teatrą

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**

##### JSON duomenys:

    {
		"name":"update",
		"address":"address"
	}

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    {
		"id" : "29",
		"name":"update",
		"address":"address"
	}

  **Kai duomenų bazė neturi duomenų:**

    {
		"type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
		"title": "Not Found",
		"status": 404,
		"traceId": "00-9862c41b7cabe182e79d824b1e71ee27-67d7bd6fa9a1c9be-00"
	}   
 
 
#### PUT api/cinemas/cinemasId/movies/id

Koreguoja nurodyta filmą.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**

##### JSON duomenys:

    {
		"description":"update"
	}

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    {
		"id": 5,
		"title": "teat",
		"genre": "teat",
		"description": "update"
	}

  **Kai duomenų bazė neturi duomenų:**

    {
		"type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
		"title": "Not Found",
		"status": 404,
		"traceId": "00-9862c41b7cabe182e79d824b1e71ee27-67d7bd6fa9a1c9be-00"
	}    


#### PUT api/cinemas/cinemasId/movies/moviesId/screenings/id

Koreguoja nurodytą seansą.

#### Panaudojimo pavyzdžiai

##### Užklausa:

/api/cinemas/**1**/movies/**1**/screenings/**1**

##### JSON duomenys:

    {
		"hall":"10",
		"seatnumber":"199",
		"price":"10"
	}

##### Atsakymas:

**Kai duomenų bazė turi duomenų:**

    {
		"id": 6,
		"hall": 10,
		"seatnumber": 199,
		"price": 10
	}

  **Kai duomenų bazė neturi duomenų:**

    {
	    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
	    "title": "Not Found",
	    "status": 404,
	    "traceId": "00-8ef365ac0cc368046c207f2296be1b9f-4301ff3d14c9b82e-00"
    }    
   
---------------------------------------------------------------------------------------------------------------------

### Autentifikacija

 #### POST auth/login

Naudotojas prisijungia prie sistemos

#### Galimi atsako kodai: 201, 404

#### Panaudojimo pavyzdžiai

##### Užklausa:

{domain}/auth/login

##### JSON duomenys:

    {
		"userName" : "TestName3",
		"password" : "Verystrong1!"
	}

#### Atsakymas

**Jei naudotojas egzistuoja**

    {
		"accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVGVzdE5hbWUzIiwianRpIjoiMmZiMTQyNzEtYjY1Yi00MDk5LTkzYTQtNTRkZTQ5MzA0ZmU5IiwidXNlcklkIjoiZmM5ZDI2ZDctNGZlMS00NDZhLWFkZmEtYTZlZTEyNDI1MDczIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU2ltcGxlVXNlciIsImV4cCI6MTY3MTgwODU2NSwiaXNzIjoiU2ltb25hcyIsImF1ZCI6IlRydXN0ZWRDbGllbnQifQ.wyf0-40YRIOWx9HMvGg7YBD1PFXwmeHLfUzNbITB8qU"
	}

**Jei naudotojas neegzistuoja**

    {
	    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
	    "title": "Unauthorized",
	    "status": 401,
	    "traceId": "00-977ff77494f66ac64de1f3efc7f93f54-3c59f865d27340c4-00"
    }

 #### POST auth/register

Užregistruojamas naujas naudotojas

#### Galimi atsako kodai: 201

#### Panaudojimo pavyzdžiai

##### Užklausa:
{domain}/auth/register

##### JSON duomenys:

	{
		"userName" : "TestName3",
		"email" : "testemail2@email.com",
		"password" : "Verystrong1!"
	}
	
##### Atsakymas:

    {
		"id": "fc9d26d7-4fe1-446a-adfa-a6ee12425073",
		"userName": "TestName3",
		"email": "testemail2@email.com"
	}

---------------------------------------------------------------------------------------------------------------------

## Projekto išvados

Funkcinius reikalavimus pavyko relizuoti. Daugiausiai sunkumų buvo kuriant front-end, dėl patirties stokos.
Projekto realizavimo metu buvo susidurta su CORS problema, tačiau po kelių dienų kančių išspręndžiau problemą.
Taip pat susiduriau su sunkumais susikurti duomenų bazę debesyje.


