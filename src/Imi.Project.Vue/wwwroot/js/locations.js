let vue = new Vue({
    el: '#app',
    data: {
        locationsLoaded: false,
        locations: null,
        selectedLocation: null,
        token: undefined,
        config: null,
        modalMessage: "",
        modalTitle: "",
        newLocation: null,
        isAdding: false,
        isUpdating: false,
        isDeleting: false,
        pageInfo: {
            pageAmount: 0,
            itemCount: 0
        },
    },
    methods: {
        loadLocationsAsync: async function () {
            let app = this;
            app.token = getCookieValue("auth-token");
            if (app.token === undefined) return;

            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/locations`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.locations = response.results;
            app.locationsLoaded = true;
        },
        deleteAsync: async function () {
            let app = this;
            app.isDeleting = true;
            let response = await axios.delete(`${apiLink}me/locations${app.selectedLocation.id}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isDeleting = false;
                    app.modalTitle = "Deletion Failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isDeleting = false;
            app.locations.pop(app.selectedLocation);
            app.pageInfo.itemCount--;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.selectedLocation = null;
        },
        updateAsync: async function () {
            let app = this;
            app.isUpdating = true;

            let form = createFormData(app.selectedLocation);
            let response = await axios.put(`${apiLink}me/locations`, form, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isUpdating = false;
                    app.modalTitle = "Update failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isUpdating = false;
            let locationIds = app.locations.map(location => location.id);
            let index = locationIds.indexOf(app.selectedLocation.id);
            app.locations.splice(index, 1, response);
            app.selectedLocation = null;
        },
        addAsync: async function () {
            let app = this;
            app.isAdding = true;

            let form = createFormData(this.newLocation);
            let response = await axios.post(`${apiLink}me/locations`, form, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isAdding = false;
                    app.modalTitle = "Add failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isAdding = false;
            app.locations.push(response);
            app.locations = app.locations.splice(0, 10);
            app.pageInfo.itemCount++;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.newLocation = null;
        },
        updateImage: function () {
            this.selectedLocation.image = this.$refs.updateImageInput.files[0];
        },
        addImage: function () {
            this.newLocation.image = this.$refs.addImageInput.files[0];
        },
        loadLocationsByPage: async function (page) {
            let app = this;
            app.locationsLoaded = false;
            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/locations?Page=${page}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.locations = response.results;
            app.selectedPage = 1;
            app.locationsLoaded = true;
        }
    },
    mounted() {
        this.loadLocationsAsync();
    }
});