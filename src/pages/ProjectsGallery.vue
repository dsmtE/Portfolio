<template>
  <div class="row">
    <ProjectTile class="col-12 col-md-2" v-for="(project, id) in projects" :key="id"
    :data="project" />
  </div>
</template>

<script>

import projects from 'assets/projects.json';

export default {
  name: 'ProjectsGallery',
  components: {
    ProjectTile: () => import('components/ProjectTile.vue'),
  },
  data() {
    return {
      projects,
    };
  },
  mounted() {
  },
};
</script>

<style>

</style>
