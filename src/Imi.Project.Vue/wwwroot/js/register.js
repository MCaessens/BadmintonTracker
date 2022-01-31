
window.addEventListener('load', Init);

function Init(){
    let password = document.getElementById("inputPassword");
    let passwordConfirm = document.getElementById("inputConfirmPassword");
    password.addEventListener('change', () => {
        password.setCustomValidity(password.value !== passwordConfirm.value ? "Passwords do not match" : "eee");
        passwordConfirm.setCustomValidity(password.value !== passwordConfirm.value ? "Passwords do not match" : "eee");
        password.reportValidity();
    });
    passwordConfirm.addEventListener('change', () => {
        password.setCustomValidity(password.value !== passwordConfirm.value ? "Passwords do not match" : "");
        passwordConfirm.setCustomValidity(password.value !== passwordConfirm.value ? "Passwords do not match" : "");
        password.reportValidity();
    });
}
let app = new Vue({
    el: '#register',
    data: {
        registerForm: {
            firstName: "",
            lastName: "",
            userName: "",
            email: "",
            password: "",
            confirmPassword: "",
            dateOfBirth: null,
        },
        registering: false,
        modalTitle: "",
        modalMessage: ""
    },
    methods:{
        registerAsync: async function (){
            let app = this;
            app.registering = true;
            let response = await axios.post(`${apiLink}auth/register`, this.registerForm)
                .then(resp => resp.data)
                .catch(() => {
                    app.registering = false;
                    app.modalTitle = "Something went wrong";
                    app.modalMessage = "The server may be offline, please check again later.";
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.registering = false;
            app.modalTitle = "Successfully Registered";
            app.modalMessage = "You have successfully registered your account. You can now proceed to login.";
            $('#notificationModal').modal('show');
        },
        toLogin: function (){
            window.location.href = "login";
        }
    }
});