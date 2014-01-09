#version 130

precision highp float;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
out vec4 color;

in vec3 in_position;

void main() {
    gl_Position = projection_matrix * model_matrix * view_matrix * vec4(in_position, 1);
	//gl_Position = vec4(in_position, 1);
	color = vec4(1, 0, 0, 1);
}