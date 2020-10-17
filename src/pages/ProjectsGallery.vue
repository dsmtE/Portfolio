<template>
  <div>
    <h1 class="q-mb-xl text-center" :style="{'margin-top': `${5}%`, 'margin-bottom': `${5}%`}">
      Projects
    </h1>
    <div class="flex flex-center justify-center">
      <Filtering class="filtering q-pa-sm q-mb-xl"
      title="Catégories" :items="filteringTags" v-model="selectedTags"/>
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

import projects from 'assets/projects.json';

export default {
  name: 'ProjectsGallery',
  components: {
    ProjectTile: () => import('components/ProjectTile.vue'),
    Filtering: () => import('components/Filtering.vue'),
  },
  data() {
    return {
      projects,
      tilesWidth: '70%',
      filteringTags: [
        '01',
        '02',
        '03',
        '04',
      ],
      selectedTags: [],
    };
  },
  mounted() {
  },
};
</script>

<style lang="scss" scoped>

.filtering {
  // border: 1px solid #eee;
  // border-radius: 10px;
}
</style>
