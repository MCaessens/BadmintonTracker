let vue = new Vue({
    el: '#app',
    data: {
        racketsLoaded: false,
        rackets: null,
        selectedRacket: null,
        token: undefined,
        config: null,
        modalMessage: "",
        modalTitle: "",
        newRacket: null,
        isAdding: false,
        isUpdating: false,
        isDeleting: false,
        pageInfo: {
            pageAmount: 0,
            itemCount: 0
        },
    },
    methods: {
        loadRacketsAsync: async function () {
            let app = this;
            app.token = getCookieValue("auth-token");
            if (app.token === undefined) return;

            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/rackets`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.rackets = response.results;
            app.racketsLoaded = true;
        },
        deleteAsync: async function () {
            let app = this;
            app.isDeleting = true;
            let response = await axios.delete(`${apiLink}me/rackets${app.selectedRacket.id}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isDeleting = false;
                    app.modalTitle = "Deletion Failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isDeleting = false;
            app.rackets.pop(app.selectedRacket);
            app.pageInfo.itemCount--;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.selectedRacket = null;
        },
        updateAsync: async function () {
            let app = this;
            app.isUpdating = true;

            let form = createFormData(app.selectedRacket);
            let response = await axios.put(`${apiLink}me/rackets`, form, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isUpdating = false;
                    app.modalTitle = "Update failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isUpdating = false;
            let racketIds = app.rackets.map(racket => racket.id);
            let index = racketIds.indexOf(app.selectedRacket.id);
            app.rackets.splice(index, 1, response);
            app.selectedRacket = null;
        },
        addAsync: async function () {
            let app = this;
            app.isAdding = true;

            let form = createFormData(this.newRacket);
            let response = await axios.post(`${apiLink}me/rackets`, form, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isAdding = false;
                    app.modalTitle = "Add failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isAdding = false;
            app.rackets.push(response);
            app.rackets = app.rackets.splice(0, 10);
            app.pageInfo.itemCount++;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.newRacket = null;
        },
        updateImage: function () {
            this.selectedRacket.image = this.$refs.updateImageInput.files[0];
        },
        addImage: function () {
            this.newRacket.image = this.$refs.addImageInput.files[0];
        },
        loadRacketsByPage: async function (page) {
            let app = this;
            app.racketsLoaded = false;
            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/rackets?Page=${page}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.rackets = response.results;
            app.selectedPage = 1;
            app.racketsLoaded = true;
        }
    },
    mounted() {
        this.loadRacketsAsync();
    }
});