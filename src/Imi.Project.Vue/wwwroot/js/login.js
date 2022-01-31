let vue = new Vue({
    el: '#login',
    data: {
        loginForm: {
            userNameOrEmail: "",
            password: ""
        },
        modalTitle: "",
        modalMessage: "",
        loggingIn: false
    },
    methods: {
        loginAsync: async function () {
            let app = this;
            app.loggingIn = true;
            let response = await axios.post("https://localhost:7001/api/auth/login", this.loginForm)
                .then(resp => resp.data.token)
                .catch(error => {
                    app.loggingIn = false;
                    app.modalTitle = "Login Failed";
                    app.modalMessage = "No account with the given username / e-mail address and password combination were found. Please try again.";
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;


            app.loggingIn = false;
            writeCookie("auth-token", response);
            app.modalTitle = "Successfully Logged In";
            app.modalMessage = "You have successfully logged in, please proceed!";
            $('#notificationModal ').modal('show');
        },
        toRegister: function () {
            window.location.href = "register";
        }
    }
});