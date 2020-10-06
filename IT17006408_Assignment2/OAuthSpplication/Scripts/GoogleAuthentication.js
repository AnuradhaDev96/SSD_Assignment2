function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isExternalUserRegistered(accessToken);
            }
        }
    }
}

function logExternalUser(accessToken) {
    localStorage.setItem('tokenData', tokenData);
}

function isExternalUserRegistered(accessToken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                localStorage.setItem('access-token', accessToken);
                localStorage.setItem('userName', response.Email);
                window.location.href = "CalendarHome.html";
            }
            else {
                signUpExternalUser(accessToken);
            }
        }
    });
}

function signUpExternalUser(accessToken) {
    $.ajax({
        url: '/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function () {
            window.location.href = "/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=https%3A%2F%2Flocalhost%3A44373%2FLogin.html&state=2H4FZIKelhg-ejeDtrsXtV4SRKlTkFhEmh-c6wwEsHs1";
        }
    });
}