<template>
  <div>
    <h1 class="q-mb-xl text-center" :style="{'margin-top': `${5}%`, 'margin-bottom': `${5}%`}">
      {{ $t('projects') }}
    </h1>
    <div class="flex flex-center justify-center">
      <Filtering class="filtering q-pa-sm q-mb-xl"
      :title="$t('categories')" :items="availableTags" v-model="selectedTags"/>
        <transition-group class="row content-center projectsTiles" name="projectsList"
          tag="div" :style="{width: tilesWidth}">
          <!-- <ProjectTile class="col-12 col-md-6" v-for="project in projects"
          :key="project.name" :data="project" />-->
          <ProjectTile class="col-12 col-md-6" v-for="i in 10"
          :key="i" :data="projects[0]" />
        </transition-group>
    </div>
  </div>
</template>

<script>

import projectsJson from 'assets/projects.json';

export default {
  name: 'ProjectsGallery',
  components: {
    ProjectTile: () => import('components/ProjectTile.vue'),
    Filtering: () => import('components/Filtering.vue'),
  },
  data() {
    return {
      projects: projectsJson.projects,
      tilesWidth: '70%',
      availableTags: projectsJson.tags,
      selectedTags: [],
    };
  },
  computed: {
    filteredProjects() {
      return this.projects.filter(p => p.tags.every(t => this.selectedTags.includes(t)));
    },
  },
  mounted() {
  },
  i18n: {
    messages: {
      'en-us': {
        categories: 'Categories',
        projects: 'Projects',
      },
      fr: {
        categories: 'Catégories',
        projects: 'Projets',
      },
    },
  },
};
</script>

<style lang="scss" scoped>

.filtering {
  // border: 1px solid #eee;
  // border-radius: 10px;
}
</style>
