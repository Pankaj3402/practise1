pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/your-repo.git'
            }
        }

        stage('Build') {
            steps {
                echo 'Build started'
            }
        }
    }
}
