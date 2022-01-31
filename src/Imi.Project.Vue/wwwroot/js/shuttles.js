let vue = new Vue({
    el: '#app',
    data: {
        shuttlesLoaded: false,
        shuttles: null,
        selectedShuttle: null,
        token: undefined,
        config: null,
        modalMessage: "",
        modalTitle: "",
        newShuttle: null,
        isAdding: false,
        isUpdating: false,
        isDeleting: false,
        pageInfo: {
            pageAmount: 0,
            itemCount: 0
        },
    },
    methods: {
        loadShuttlesAsync: async function () {
            let app = this;
            app.token = getCookieValue("auth-token");
            if (app.token === undefined) return;

            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/shuttlecocks`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.shuttles = response.results;
            app.shuttlesLoaded = true;
        },
        deleteAsync: async function () {
            let app = this;
            app.isDeleting = true;
            let response = await axios.delete(`${apiLink}me/shuttlecocks${app.selectedShuttle.id}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isDeleting = false;
                    app.modalTitle = "Deletion Failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isDeleting = false;
            app.shuttles.pop(app.selectedShuttle);
            app.pageInfo.itemCount--;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.selectedShuttle = null;
        },
        updateAsync: async function () {
            let app = this;
            app.isUpdating = true;

            let form = createFormData(app.selectedShuttle);
            let response = await axios.put(`${apiLink}me/shuttlecocks`, form, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isUpdating = false;
                    app.modalTitle = "Update failed";
                    app.modalMessage = handleError(error.response.data);
                    console.log(error.response.data)
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isUpdating = false;
            let shuttleIds = app.shuttles.map(shuttle => shuttle.id);
            let index = shuttleIds.indexOf(app.selectedShuttle.id);
            app.shuttles.splice(index, 1, response);
            app.selectedShuttle = null;
        },
        addAsync: async function () {
            let app = this;
            app.isAdding = true;

            let form = createFormData(this.newShuttle);
            let response = await axios.post(`${apiLink}me/shuttlecocks`, form, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isAdding = false;
                    app.modalTitle = "Add failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isAdding = false;
            app.shuttles.push(response);
            app.shuttles = app.shuttles.splice(0, 10);
            app.pageInfo.itemCount++;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.newShuttle = null;
        },
        updateImage: function () {
            this.selectedShuttle.image = this.$refs.updateImageInput.files[0];
        },
        addImage: function () {
            this.newShuttle.image = this.$refs.addImageInput.files[0];
        },
        loadShuttlesByPage: async function (page) {
            let app = this;
            app.shuttlesLoaded = false;
            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/shuttlecocks?Page=${page}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.shuttles = response.results;
            app.selectedPage = 1;
            app.shuttlesLoaded = true;
        }
    },
    mounted() {
        this.loadShuttlesAsync();
    }
});